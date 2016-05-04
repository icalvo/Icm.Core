// Copyright 2015 gekka.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using VB = Microsoft.CodeAnalysis.VisualBasic;
using VBS = Microsoft.CodeAnalysis.VisualBasic.Syntax;
using CS = Microsoft.CodeAnalysis.CSharp;
using CSS = Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gekka.Roslyn.Translator.Vb2Cs
{
    partial class Translator
    {
        #region Declaration

        static CSS.CompilationUnitSyntax ConvertCompilationUnit(this VBS.CompilationUnitSyntax node)
        {
            if (node == null)
            {
                return null;
            }

            var vbCompilationUnit = node;

            var csUsings = vbCompilationUnit.Imports.ConvertSyntaxNodes<CSS.UsingStatementSyntax>();
            var vbAttributes = vbCompilationUnit.Attributes.Conv
            var vbAttributeStatement = CS.SyntaxFactory.AttributesStatement(vbAttributes);
            var vbmembers = vbCompilationUnit.Members.ConvertMembers();

            var csCompilationUnit = CS.SyntaxFactory.CompilationUnit();
            csCompilationUnit = csCompilationUnit.AddImports(csUsings.ToArray());
            csCompilationUnit = csCompilationUnit.AddAttributes(vbAttributeStatement);
            csCompilationUnit = csCompilationUnit.AddMembers(vbmembers.ToArray());

            return csCompilationUnit;
        }

        static CSS.ImportsStatementSyntax ConvertUsingDirective(this VBS.UsingDirectiveSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csUsing = (VBS.UsingDirectiveSyntax)node;

            var vbAlias = (CSS.ImportAliasClauseSyntax)csUsing.Alias.Convert();
            var vbName = (CSS.NameSyntax)csUsing.Name.Convert();
            var vbImportsClause = CS.SyntaxFactory.SimpleImportsClause(vbAlias, vbName);

            var vbImports = CS.SyntaxFactory.ImportsStatement();
            return vbImports.AddImportsClauses(vbImportsClause);
        }

        static CSS.NamespaceBlockSyntax ConvertNamespaceDeclaration(this VBS.NamespaceDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csNamespace = (VBS.NamespaceDeclarationSyntax)node;

            var vbName = (CSS.NameSyntax)csNamespace.Name.Convert();
            var vbNamespaceStatement = CS.SyntaxFactory.NamespaceStatement(vbName);//Namespace ***
            var vbmembers = csNamespace.Members.ConvertMembers().ConvertToSyntaxList();//Namespace
            return CS.SyntaxFactory.NamespaceBlock(vbNamespaceStatement, vbmembers.ConvertToSyntaxList());
        }

        #region TypeDeclaration

        static CSS.EnumBlockSyntax ConvertEnumDeclaration(this VBS.EnumDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csEnum = (VBS.EnumDeclarationSyntax)node;

            var vbAttributes = csEnum.AttributeLists.ConvertAttributes();
            var vbInheritTypes = (CSS.InheritsStatementSyntax)csEnum.BaseList.Convert();
            var vbInheritsAndImplements = vbInheritTypes.SplitInherit(true);
            var vbMembers = csEnum.Members.ConvertMembers();

            SyntaxToken vbDefaultModifier;
            if (csEnum.Parent == null || csEnum.Parent.IsKind(VB.SyntaxKind.CompilationUnit) || csEnum.Parent.IsKind(VB.SyntaxKind.NamespaceDeclaration))
            {
                vbDefaultModifier = CS.SyntaxFactory.Token(CS.SyntaxKind.FriendKeyword);
            }
            else
            {
                vbDefaultModifier = CS.SyntaxFactory.Token(CS.SyntaxKind.PrivateKeyword);
            }

            var vbModifiers = ConvertModifiersWithDefault(csEnum.Modifiers, vbDefaultModifier);
            var vbIdentifier = CS.SyntaxFactory.Identifier(csEnum.Identifier.Text);
            CSS.AsClauseSyntax vbAsClause = null;
            if (vbInheritTypes != null)
            {
                vbAsClause = CS.SyntaxFactory.SimpleAsClause(vbInheritTypes.Types[0]);
            }

            var vbEnumStatement = CS.SyntaxFactory.EnumStatement(vbAttributes, vbModifiers, vbIdentifier, vbAsClause);
            return CS.SyntaxFactory.EnumBlock(vbEnumStatement, vbMembers);
        }

        static CSS.TypeBlockSyntax ConvertTypeDeclaration(this VBS.BaseTypeDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csType = (VBS.TypeDeclarationSyntax)node;

            var vbAttributes = csType.AttributeLists.ConvertAttributes();
            var vbInhritTypes = (CSS.InheritsStatementSyntax)csType.BaseList.Convert();
            var vbInheritsAndImplements = vbInhritTypes.SplitInherit(true);
            var vbModifiers = ConvertModifiersWithDefault(csType.Modifiers, GetDefaultModifiers(node));
            var vbIdentifier = CS.SyntaxFactory.Identifier(csType.Identifier.Text);
            var vbTypeParameterList = ConvertTypeParameters(csType.TypeParameterList, csType.ConstraintClauses);
            var vbMembers = new SyntaxList<CSS.StatementSyntax>();


            switch (node.Kind())
            {
            case VB.SyntaxKind.ClassDeclaration:
                {
                    vbMembers = vbMembers.AddRange(((VBS.ClassDeclarationSyntax)csType).ConvertClassMembers());
                    return CS.SyntaxFactory.ClassBlock
                                (CS.SyntaxFactory.ClassStatement(vbAttributes, vbModifiers, vbIdentifier, vbTypeParameterList)
                                , vbInheritsAndImplements.Inherits.ConvertToSyntaxList()
                                , vbInheritsAndImplements.Implements.ConvertToSyntaxList()
                                , vbMembers);
                }
            case VB.SyntaxKind.InterfaceDeclaration:
                {
                    //remove member's body block
                    //Interfaceは宣言のみなので宣言以外の要素を消す
                    vbMembers = csType.Members.ConvertMembers().ConvertToInterfaceMembers();
                    return CS.SyntaxFactory.InterfaceBlock
                                (CS.SyntaxFactory.InterfaceStatement(vbAttributes, vbModifiers, vbIdentifier, vbTypeParameterList)
                                , vbInheritsAndImplements.Inherits.ConvertToSyntaxList()
                                , vbInheritsAndImplements.Implements.ConvertToSyntaxList()
                                , vbMembers);
                }
            case VB.SyntaxKind.StructDeclaration:
                {
                    vbMembers = vbMembers.AddRange(csType.Members.ConvertMembers());
                    return CS.SyntaxFactory.StructureBlock
                                (CS.SyntaxFactory.StructureStatement(vbAttributes, vbModifiers, vbIdentifier, vbTypeParameterList)
                                , vbInheritsAndImplements.Inherits.ConvertToSyntaxList()
                                , vbInheritsAndImplements.Implements.ConvertToSyntaxList()
                                , vbMembers);
                }
            default:
                System.Diagnostics.Debug.Assert(false, "ConvertTypeDeclaration");
                return null;
            }
        }

        private static SyntaxList<CSS.StatementSyntax> ConvertToInterfaceMembers(this SyntaxList<CSS.StatementSyntax> vbMembers)
        {
            //Remove Modifiers
            //Remove Method Block (End ***)
            var noModifiers = new SyntaxTokenList();
            var vbMembersCopy = new SyntaxList<CSS.StatementSyntax>();
            foreach (CSS.StatementSyntax vbMember in vbMembers)
            {

                if (vbMember.IsKind(CS.SyntaxKind.SubBlock) || vbMember.IsKind(CS.SyntaxKind.FunctionBlock))
                {
                    var block = (CSS.MethodBlockSyntax)vbMember;
                    var vbStatement = block.SubOrFunctionStatement;
                    vbStatement = vbStatement.WithModifiers(noModifiers);
                    vbMembersCopy = vbMembersCopy.Add(vbStatement);
                }
                else if (vbMember.IsKind(CS.SyntaxKind.PropertyStatement) || vbMember.IsKind(CS.SyntaxKind.PropertyBlock))
                {
                    CSS.PropertyStatementSyntax vbProperty;
                    if (vbMember.IsKind(CS.SyntaxKind.PropertyBlock))
                    {
                        vbProperty = ((CSS.PropertyBlockSyntax)vbMember).PropertyStatement;
                    }
                    else
                    {
                        vbProperty = (CSS.PropertyStatementSyntax)vbMember;
                    }

                    SyntaxToken readOnly = vbProperty.Modifiers.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.ReadOnlyKeyword));
                    SyntaxToken writeOnly = vbProperty.Modifiers.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.WriteOnlyKeyword));

                    vbProperty = vbProperty.WithModifiers(noModifiers);
                    if (readOnly.IsKind(CS.SyntaxKind.ReadOnlyKeyword))
                    {
                        vbProperty = vbProperty.AddModifiers(readOnly);
                    }
                    if (writeOnly.IsKind(CS.SyntaxKind.WriteOnlyKeyword))
                    {
                        vbProperty = vbProperty.AddModifiers(writeOnly);
                    }
                    vbMembersCopy = vbMembersCopy.Add(vbProperty);
                }
                else if (vbMember.IsKind(CS.SyntaxKind.EventStatement))
                {
                    var vbEvent = (CSS.EventStatementSyntax)vbMember;
                    vbEvent = vbEvent.WithModifiers(noModifiers);
                    vbMembersCopy = vbMembersCopy.Add(vbEvent);
                }
            }
            return vbMembersCopy;
        }

        private static CSS.TypeParameterListSyntax ConvertTypeParameters
            (VBS.TypeParameterListSyntax csTypeParameterList
            , IEnumerable<VBS.TypeParameterConstraintClauseSyntax> csConstraintClauses)
        {
            var vbTypeParameterList = (CSS.TypeParameterListSyntax)csTypeParameterList.Convert();//ジェネリックの型
            foreach (VBS.TypeParameterConstraintClauseSyntax csConstraintClause in csConstraintClauses)
            {
                var vbConstraint = (CSS.TypeParameterConstraintClauseSyntax)csConstraintClause.Convert();
                VBS.IdentifierNameSyntax genericTypeName = csConstraintClause.Name;

                foreach (CSS.TypeParameterSyntax vbTypeParameter in vbTypeParameterList.Parameters)
                {
                    if (vbTypeParameter.Identifier.Text == csConstraintClause.Name.Identifier.Text)
                    {
                        var vbTypeParameterNew = vbTypeParameter.WithTypeParameterConstraintClause(vbConstraint);
                        var vbTypeParametersNew = vbTypeParameterList.Parameters.Replace(vbTypeParameter, vbTypeParameterNew);
                        vbTypeParameterList = vbTypeParameterList.WithParameters(vbTypeParametersNew);
                        break;
                    }
                }
            }
            return vbTypeParameterList;
        }


        static CSS.ConstructorBlockSyntax ConvertConstructor(this VBS.ConstructorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csConstructor = (VBS.ConstructorDeclarationSyntax)node;
            var vbAttributes = csConstructor.AttributeLists.ConvertAttributes().ConvertToSyntaxList();
            var vbStatements = csConstructor.Body.ConvertBlockOrStatements();
            //var vbID = csConstructor.Identifier.ConvertID();
            var vbInitializer = csConstructor.Initializer.ConvertConstructorInitializer();
            var vbModifiers = ConvertModifiers(csConstructor.Modifiers);
            var vbParameters = (CSS.ParameterListSyntax)csConstructor.ParameterList.Convert();
            if (vbInitializer != null)
            {
                vbStatements = vbStatements.Insert(0, vbInitializer);
            }
            var vbSubNewStatement = CS.SyntaxFactory.SubNewStatement(vbAttributes, vbModifiers, vbParameters);
            var vbNew = CS.SyntaxFactory.ConstructorBlock(vbSubNewStatement, vbStatements);
            return vbNew;
        }

        static CSS.ExpressionStatementSyntax ConvertConstructorInitializer(this VBS.ConstructorInitializerSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csConstructorInitializer = (VBS.ConstructorInitializerSyntax)node;
            var vbArguments = (CSS.ArgumentListSyntax)csConstructorInitializer.ArgumentList.Convert();
            CSS.ExpressionSyntax vbTarget;
            if (node.Kind() == VB.SyntaxKind.BaseConstructorInitializer)
            {
                vbTarget = CS.SyntaxFactory.MyBaseExpression();
            }
            else
            {
                vbTarget = CS.SyntaxFactory.MeExpression();
            }
            var vbMember = CS.SyntaxFactory.IdentifierName(CS.SyntaxFactory.Token(CS.SyntaxKind.NewKeyword).ToFullString());
            var vbExpression = CS.SyntaxFactory.SimpleMemberAccessExpression(vbTarget, vbMember);
            vbTarget = CS.SyntaxFactory.InvocationExpression(vbExpression, vbArguments);
            return CS.SyntaxFactory.ExpressionStatement(vbTarget);
        }

        static CSS.MethodBlockSyntax ConvertDestructor(this VBS.DestructorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDestructor = (VBS.DestructorDeclarationSyntax)node;
            var vbAttributes = csDestructor.AttributeLists.ConvertAttributes();
            var vbModifiers = new SyntaxTokenList();
            vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.ProtectedKeyword));
            vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.OverridesKeyword));

            var vbParameters = (CSS.ParameterListSyntax)csDestructor.ParameterList.Convert();
            var vbStatements = csDestructor.Body.ConvertBlockOrStatements();
            var vbID = CS.SyntaxFactory.IdentifierName("Finalize");//FinalizeKeyword Token not found?
            var vbSubStatement = CS.SyntaxFactory.SubStatement("Finalize")
                                .AddAttributeLists(vbAttributes.ToArray())
                                .AddModifiers(vbModifiers.ToArray())
                                .AddParameterListParameters(vbParameters.Parameters.ToArray());
            return CS.SyntaxFactory.SubBlock(vbSubStatement, vbStatements);
        }

        static CS.VisualBasicSyntaxNode ConvertMethodDeclaration(this VBS.MethodDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csMethod = (VBS.MethodDeclarationSyntax)node;

            bool isInterfaceMethod = csMethod.ExplicitInterfaceSpecifier != null;
            bool isSub = (csMethod.ReturnType is VBS.PredefinedTypeSyntax)
                            && ((VBS.PredefinedTypeSyntax)csMethod.ReturnType).Keyword.IsKind(VB.SyntaxKind.VoidKeyword);

            var vbImplements = (CSS.ImplementsClauseSyntax)csMethod.ExplicitInterfaceSpecifier.ConvertExplicitInterfaceSpecifier();

            var vbAttributeListSplit = csMethod.AttributeLists.ConvertAttributesSplit();
            var vbModifiers = ConvertModifiersWithDefault(csMethod.Modifiers);
            var vbIdentifier = CS.SyntaxFactory.Identifier(csMethod.Identifier.Text);
            var vbTypeParameterList = ConvertTypeParameters(csMethod.TypeParameterList, csMethod.ConstraintClauses);

            var vbParameters = (CSS.ParameterListSyntax)csMethod.ParameterList.Convert();
            var vbStatements = csMethod.Body.ConvertBlockStatements();

            if (vbModifiers.Count == 0 && !isInterfaceMethod)
            {
                if (csMethod.Parent.IsKind(VB.SyntaxKind.InterfaceDeclaration))
                {
                    vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.PublicKeyword));
                }
                else
                {
                    vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.PrivateKeyword));
                }
            }

            foreach (CSS.StatementSyntax vbStatement in vbStatements)
            {
                if (HasKindChildNode(vbStatement, CS.SyntaxKind.YieldStatement))
                {
                    vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.IteratorKeyword));
                    break;
                }
            }



            CSS.MethodBlockSyntax vbMethodBlock;
            if (isSub)
            {
                var vbSubStatement = CS.SyntaxFactory.SubStatement(vbAttributeListSplit.Others, vbModifiers, vbIdentifier, vbTypeParameterList, vbParameters, null, null, vbImplements);
                vbMethodBlock = CS.SyntaxFactory.SubBlock(vbSubStatement);
            }
            else
            {
                var vbReturnType = (CSS.TypeSyntax)csMethod.ReturnType.Convert();
                var vbReturnAs = CS.SyntaxFactory.SimpleAsClause(vbAttributeListSplit.Return, vbReturnType);

                var vbFunctionStatement = CS.SyntaxFactory.FunctionStatement(vbAttributeListSplit.Others, vbModifiers, vbIdentifier, vbTypeParameterList, vbParameters, null, null, vbImplements);
                vbFunctionStatement = vbFunctionStatement.WithAsClause(vbReturnAs);
                vbMethodBlock = CS.SyntaxFactory.FunctionBlock(vbFunctionStatement);
            }

            if (csMethod.Modifiers.Count(_ => _.IsKind(VB.SyntaxKind.AbstractKeyword)) > 0)
            {
                return vbMethodBlock.BlockStatement;
            }

            vbMethodBlock = vbMethodBlock.AddStatements(vbStatements.ToArray());
            return vbMethodBlock;
        }

        static CSS.OperatorBlockSyntax ConvertOperator(this VBS.OperatorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csOperator = (VBS.OperatorDeclarationSyntax)node;

            var vbAttributeListSplit = csOperator.AttributeLists.ConvertAttributesSplit();
            var vbModifiers = ConvertModifiersWithDefault(csOperator.Modifiers);
            var vbParameters = (CSS.ParameterListSyntax)csOperator.ParameterList.Convert();
            var vbStatements = csOperator.Body.ConvertBlockStatements();
            var vbExpressinBody = csOperator.ExpressionBody.Convert();

            var vbOperatorKind = OperatorPairs.First(_ => csOperator.OperatorToken.IsKind(_.Cs)).Vb;
            var vbOperatorToken = CS.SyntaxFactory.Token(vbOperatorKind);

            if (vbModifiers.Count == 0)
            {
                vbModifiers = vbModifiers.Add(GetDefaultModifiers(node));
            }
            foreach (CSS.StatementSyntax vbStatement in vbStatements)
            {
                if (HasKindChildNode(vbStatement, CS.SyntaxKind.YieldStatement))
                {
                    vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.IteratorKeyword));
                    break;
                }
            }

            var vbReturnType = (CSS.TypeSyntax)csOperator.ReturnType.Convert();
            var vbReturnAs = CS.SyntaxFactory.SimpleAsClause(vbAttributeListSplit.Return, vbReturnType);

            var vbOperatorStatement = CS.SyntaxFactory.OperatorStatement(vbAttributeListSplit.Others, vbModifiers, vbOperatorToken, vbParameters, vbReturnAs);
            vbOperatorStatement = vbOperatorStatement.WithAsClause(vbReturnAs);
            var vbOperatorBlock = CS.SyntaxFactory.OperatorBlock(vbOperatorStatement);
            vbOperatorBlock = vbOperatorBlock.AddStatements(vbStatements.ToArray());
            return vbOperatorBlock;
        }

        static CS.VisualBasicSyntaxNode ConvertConversionOperator(this VBS.ConversionOperatorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csConversion = (VBS.ConversionOperatorDeclarationSyntax)node;
            var vbAttributeListSplit = csConversion.AttributeLists.ConvertAttributesSplit();
            var vbModifiers = ConvertModifiersWithDefault(csConversion.Modifiers);
            var vbParameters = (CSS.ParameterListSyntax)csConversion.ParameterList.Convert();
            var vbStatements = csConversion.Body.ConvertBlockStatements();
            var vbExpressinBody = csConversion.ExpressionBody.Convert();

            var vbToken = CS.SyntaxFactory.Token(CS.SyntaxKind.CTypeKeyword);
            vbModifiers = vbModifiers.Add(ConvertModifier(csConversion.ImplicitOrExplicitKeyword));

            var vbReturnType = (CSS.TypeSyntax)csConversion.Type.Convert();
            var vbReturnAs = CS.SyntaxFactory.SimpleAsClause(vbAttributeListSplit.Return, vbReturnType);

            var vbOperatorStatement = CS.SyntaxFactory.OperatorStatement(vbAttributeListSplit.Others, vbModifiers, vbToken, vbParameters, vbReturnAs);
            vbOperatorStatement = vbOperatorStatement.WithAsClause(vbReturnAs);
            var vbOperatorBlock = CS.SyntaxFactory.OperatorBlock(vbOperatorStatement);
            vbOperatorBlock = vbOperatorBlock.AddStatements(vbStatements.ToArray());
            return vbOperatorBlock;
        }

        static CS.VisualBasicSyntaxNode ConvertLambdaExpression(this VBS.LambdaExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLambda = (VBS.LambdaExpressionSyntax)node;
            List<CSS.ParameterSyntax> vbParameters = new List<CSS.ParameterSyntax>();

            if (csLambda is VBS.ParenthesizedLambdaExpressionSyntax)
            {
                var csLambdaParenthesized = (VBS.ParenthesizedLambdaExpressionSyntax)node;
                var vbParameterList = (CSS.ParameterListSyntax)csLambdaParenthesized.ParameterList.Convert();
                vbParameters.AddRange(vbParameterList.Parameters);
            }
            else if (csLambda is VBS.SimpleLambdaExpressionSyntax)
            {
                var csLambdaSimple = (VBS.SimpleLambdaExpressionSyntax)node;
                vbParameters.Add(csLambdaSimple.Parameter.ConvertParameter());
            }


            bool isFunction = false;
            SyntaxList<CSS.StatementSyntax> statements = new SyntaxList<CSS.StatementSyntax>();
            if (csLambda.Body.Kind() == VB.SyntaxKind.Block)
            {
                var vbStatements = ((VBS.BlockSyntax)csLambda.Body).Statements.ConvertSyntaxNodes<CSS.StatementSyntax>();
                statements = statements.AddRange(vbStatements.ToArray());
            }
            else
            {
                var vbNode = (CS.VisualBasicSyntaxNode)csLambda.Body.Convert();

                if (vbNode is CSS.ExpressionSyntax)
                {
                    switch (vbNode.Kind())
                    {
                    case CS.SyntaxKind.NumericLiteralExpression:
                        {
                            var vbStatement = CS.SyntaxFactory.ReturnStatement((CSS.ExpressionSyntax)vbNode);
                            statements = statements.Add(vbStatement);
                            isFunction = true;
                        }
                        break;
                    default:
                        {
                            var vbStatement = CS.SyntaxFactory.ExpressionStatement((CSS.ExpressionSyntax)vbNode);
                            statements = statements.Add(vbStatement);
                            break;
                        }
                    }
                }
                else
                {
                    var vbStatement = (CSS.StatementSyntax)csLambda.Body.Convert();
                    statements = statements.Add(vbStatement);
                }
            }

            if (!isFunction)
            {
                isFunction = IsFunctionLambda(csLambda);
            }

            if (isFunction)
            {
                var x = CS.SyntaxFactory.FunctionLambdaHeader().AddParameterListParameters(vbParameters.ToArray());
                var y = CS.SyntaxFactory.EndFunctionStatement();
                var z = CS.SyntaxFactory.MultiLineFunctionLambdaExpression(x, y);
                z = z.AddStatements(statements.ToArray());
                return z;
            }
            else
            {
                var x = CS.SyntaxFactory.SubLambdaHeader().AddParameterListParameters(vbParameters.ToArray());
                var y = CS.SyntaxFactory.EndSubStatement();
                var z = CS.SyntaxFactory.MultiLineSubLambdaExpression(x, y);
                z = z.AddStatements(statements.ToArray());
                return z;
            }
        }

        static bool IsFunctionLambda(VB.CSharpSyntaxNode node)
        {
            SearchReturnWalker searcher = new SearchReturnWalker();
            searcher.parentNoe = node;
            searcher.Visit(node);
            return searcher.found;
        }

        class SearchReturnWalker : VB.CSharpSyntaxWalker
        {
            public VB.CSharpSyntaxNode parentNoe;
            public bool found = false;
            public override void VisitReturnStatement(VBS.ReturnStatementSyntax node)
            {
                if (!found && node.Expression != null)
                {
                    if (node.Ancestors().FirstOrDefault(_ => _ is VBS.LambdaExpressionSyntax) == parentNoe)
                    {
                        found = true;
                    }
                }

                base.VisitReturnStatement(node);
            }
        }

        #endregion

        static CSS.WithBlockSyntax ConvertBlock(VBS.BlockSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBlock = (VBS.BlockSyntax)node;
            if (csBlock.Parent.IsKind(VB.SyntaxKind.Block) || csBlock.Parent.IsKind(VB.SyntaxKind.SwitchSection))
            {
                return CreateWithNothing().AddStatements(csBlock.ConvertBlockStatements().ToArray());
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "Please use ConvertBlock() or Block.Statements.Convert<V,C>");
                return null;
            }
        }

        #region Flow
        #region IF
        static CSS.MultiLineIfBlockSyntax ConvertIfStatement(this VBS.IfStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csIf = (VBS.IfStatementSyntax)node;
            var vbCondition = (CSS.ExpressionSyntax)csIf.Condition.Convert();
            var vbStatements = csIf.Statement.ConvertBlockOrStatements();
            var vbIfThen = CS.SyntaxFactory.IfStatement(vbCondition).WithThenKeyword(CS.SyntaxFactory.Token(CS.SyntaxKind.ThenKeyword));
            var vbIfBlock = CS.SyntaxFactory.MultiLineIfBlock(vbIfThen);
            vbIfBlock = vbIfBlock.AddStatements(vbStatements.ToArray());
            if (csIf.Else != null)
            {
                var vbElse = (CSS.ElseBlockSyntax)csIf.Else.Convert();
                var vbElseIfBlocksAndElseBlock = MergeElseIf(vbElse);

                vbIfBlock = vbIfBlock.AddElseIfBlocks(vbElseIfBlocksAndElseBlock.ElseIfBlocks.ToArray())
                                    .WithElseBlock(vbElseIfBlocksAndElseBlock.ElseBlock);
            }
            return vbIfBlock;
        }

        static CSS.ElseBlockSyntax ConvertElseClauseStatement(this VBS.ElseClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csElse = (VBS.ElseClauseSyntax)node;
            var vbStatements = csElse.Statement.ConvertBlockOrStatements();
            var vbElse = CS.SyntaxFactory.ElseBlock();
            return vbElse.AddStatements(vbStatements.ToArray());
        }
        #endregion endif

        #region  switch
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <remarks>
        /// <code lang="CS">
        /// switch(x)
        /// {
        /// case 1:
        ///     goto case 2;
        /// case 2;
        ///     goto default;
        /// default:
        ///     break;
        /// }
        /// <code>
        /// <code lang="VB">
        /// With Nothing
        ///     Dim __SELECT_VALUE_ = x
        ///     __RETRY_SELECT_n:
        ///     Select case(__SELECT_VALUE_n)
        ///     case 1
        ///         temp = 2
        ///         Goto __RETRY_SELECT_n
        ///     case 2
        ///         Goto __GOTO_DEFAULT_n
        ///     case else
        ///     __GOTODEFAULT_n
        ///     End Select 
        /// End With 
        /// </code>
        /// </remarks>
        static CS.VisualBasicSyntaxNode ConvertSwitchStatementAndRewriteGoto(this VBS.SwitchStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csSwitch = (VBS.SwitchStatementSyntax)node;

            GotoCaseRewriter rewriter = new GotoCaseRewriter(csSwitch);
            csSwitch = (VBS.SwitchStatementSyntax)rewriter.Visit(csSwitch);
            if (rewriter.HasGotoDefault)
            {
                SyntaxList<VBS.SwitchSectionSyntax> list = new SyntaxList<VBS.SwitchSectionSyntax>();
                foreach (VBS.SwitchSectionSyntax csCase in csSwitch.Sections)
                {
                    VBS.SwitchSectionSyntax csCaseTemp = csCase;

                    foreach (VBS.SwitchLabelSyntax csLabel in csCase.Labels)
                    {
                        if (csLabel.IsKind(VB.SyntaxKind.DefaultSwitchLabel))
                        {
                            var csDefaultLabelStatement = VB.SyntaxFactory.LabeledStatement(rewriter.LabelDefault, csCase.Statements[0]);
                            var csStatements = csCase.Statements.RemoveAt(0);
                            csStatements = csStatements.Insert(0, csDefaultLabelStatement);
                            csCaseTemp = csCase.WithStatements(csStatements);
                            break;
                        }
                    }
                    list = list.Add(csCaseTemp);
                }

                csSwitch = csSwitch.WithSections(list);
            }

            var vbSelectBlock = ConvertSwitchStatementNoRewrite(csSwitch);

            if (rewriter.HasGotoTop)
            {
                var vbID = CS.SyntaxFactory.ModifiedIdentifier(rewriter.VariantName);
                var vbEqual = CS.SyntaxFactory.EqualsValue(vbSelectBlock.SelectStatement.Expression);
                var vbVariable = CS.SyntaxFactory.VariableDeclarator().AddNames(vbID).WithInitializer(vbEqual);
                SeparatedSyntaxList<CSS.VariableDeclaratorSyntax> vbDeclarators
                    = new SeparatedSyntaxList<CSS.VariableDeclaratorSyntax>().Add(vbVariable);
                SyntaxTokenList vbModifiers = new SyntaxTokenList().Add(CS.SyntaxFactory.Token(CS.SyntaxKind.DimKeyword));
                var vbDim = CS.SyntaxFactory.LocalDeclarationStatement(vbModifiers, vbDeclarators);

                var vbLabel = CS.SyntaxFactory.LabelStatement(rewriter.LabelTop);

                var vbSelectStatement = vbSelectBlock.SelectStatement;
                vbSelectStatement = vbSelectStatement.WithExpression(CS.SyntaxFactory.IdentifierName(rewriter.VariantName));
                vbSelectBlock = vbSelectBlock.WithSelectStatement(vbSelectStatement);

                var vbWithNothing = CreateWithNothing();
                vbWithNothing = vbWithNothing.AddStatements(vbDim);
                vbWithNothing = vbWithNothing.AddStatements(vbLabel);
                return vbWithNothing.AddStatements(vbSelectBlock);
            }
            else
            {
                return vbSelectBlock;
            }

        }

        static CSS.SelectBlockSyntax ConvertSwitchStatementNoRewrite(this VBS.SwitchStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csSwitch = (VBS.SwitchStatementSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csSwitch.Expression.ConvertAssignRightExpression();
            var vbCases = csSwitch.Sections.ConvertSyntaxNodes<CSS.CaseBlockSyntax>();

            var vbSelect = CS.SyntaxFactory.SelectStatement(vbExpression);
            var vbSelectBlock = CS.SyntaxFactory.SelectBlock(vbSelect);
            return vbSelectBlock.AddCaseBlocks(vbCases.ToArray());
        }

        static CSS.CaseBlockSyntax ConvertSwitchSelection(this VBS.SwitchSectionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csSwitchCase = (VBS.SwitchSectionSyntax)node;

            var vbClauses = csSwitchCase.Labels.Convert<VBS.SwitchLabelSyntax, CSS.CaseClauseSyntax>();
            CSS.CaseStatementSyntax vbCaseElse = null;
            if (vbClauses.Count >= 2)
            {
                foreach (CSS.CaseClauseSyntax vbClause in vbClauses)
                {
                    if (vbClause.IsKind(CS.SyntaxKind.ElseCaseClause))
                    {
                        vbClauses = vbClauses.Remove(vbClause);
                        vbCaseElse = CS.SyntaxFactory.CaseStatement(vbClause);
                        break;
                    }
                }
            }

            var vbCase = CS.SyntaxFactory.CaseStatement();
            vbCase = vbCase.AddCases(vbClauses.ToArray());

            var vbStatements = csSwitchCase.Statements.ConvertStatements<CSS.StatementSyntax>();

            var vbCaseBlock = CS.SyntaxFactory.CaseBlock(vbCase);
            if (vbCaseElse != null)
            {
                vbCaseBlock = vbCaseBlock.AddStatements(vbCaseElse);
            }
            return vbCaseBlock.AddStatements(vbStatements.ToArray());
        }

        static CSS.SimpleCaseClauseSyntax ConvertCaseSwitchLabel(this VBS.CaseSwitchLabelSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCaseLabel = (VBS.CaseSwitchLabelSyntax)node;
            var vbCaseLabel = (CSS.ExpressionSyntax)csCaseLabel.Value.Convert();
            return CS.SyntaxFactory.SimpleCaseClause(vbCaseLabel);
        }

        class GotoCaseRewriter : VB.CSharpSyntaxRewriter
        {
            public static uint CreateLabelNumber()
            {
                return (uint)Math.Abs(System.Threading.Interlocked.Increment(ref _LabelNumber));
            }
            private static int _LabelNumber;
            public static void ResetCounter()
            {
                _LabelNumber = 0;
            }

            public GotoCaseRewriter(VBS.SwitchStatementSyntax csSwitch)
            {
                if (csSwitch == null) throw new ArgumentNullException("csSwitch");
                _csSwitch = csSwitch;
            }

            private VBS.SwitchStatementSyntax _csSwitch;

            public bool HasGotoTop { get; private set; }
            public bool HasGotoDefault { get; private set; }
            public string VariantName { get; private set; }
            public string LabelTop { get; private set; }
            public string LabelDefault { get; private set; }

            public override SyntaxNode VisitGotoStatement(VBS.GotoStatementSyntax node)
            {
                if (node.Ancestors().FirstOrDefault(_ => _.IsKind(VB.SyntaxKind.SwitchStatement)) == _csSwitch)
                {
                    bool isGotoCase = node.CaseOrDefaultKeyword.IsKind(VB.SyntaxKind.CaseKeyword);
                    bool isGotoDefault = node.CaseOrDefaultKeyword.IsKind(VB.SyntaxKind.DefaultKeyword);
                    // bool isGotoCaseOrDefault = isGotoCase | isGotoDefault;

                    if (isGotoCase || isGotoDefault)
                    {
                        if (!HasGotoTop && !HasGotoDefault)
                        {
                            string sNumber = CreateLabelNumber().ToString();
                            VariantName = "__SELECT_VALUE_" + sNumber;
                            LabelTop = "__RETRY_SELECT_" + sNumber;
                            LabelDefault = "__GOTO_DEFALUT_" + sNumber;
                        }

                        HasGotoTop |= isGotoCase;
                        HasGotoDefault |= isGotoDefault;

                        if (isGotoCase)
                        {
                            // "goto case EXPERESSION;"  --> " { VariantName = EXPERESSION;  Goto LabelTop; }"
                            var csVariant = VB.SyntaxFactory.IdentifierName(VariantName);
                            var csAssignExp = VB.SyntaxFactory.AssignmentExpression(VB.SyntaxKind.SimpleAssignmentExpression, csVariant, node.Expression);
                            var csAssignStatement = VB.SyntaxFactory.ExpressionStatement(csAssignExp);
                            var csGotoTop = VB.SyntaxFactory.GotoStatement(VB.SyntaxKind.GotoStatement, VB.SyntaxFactory.IdentifierName(LabelTop));

                            return VB.SyntaxFactory.Block().AddStatements(csAssignStatement, csGotoTop);
                        }
                        else
                        {
                            // "goto default;"  --> "goto LABEL_DEFAULT;" 
                            return VB.SyntaxFactory.GotoStatement(VB.SyntaxKind.GotoStatement, VB.SyntaxFactory.IdentifierName(LabelDefault));
                        }
                    }
                }
                return base.VisitGotoStatement(node);
            }
        }

        #endregion

        static CSS.ExitStatementSyntax ConvertBreak(this VBS.BreakStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBreak = (VBS.BreakStatementSyntax)node;

            SyntaxNode parentNode = node.Parent;
            while (parentNode != null)
            {
                var pair = BreakPairs.FirstOrDefault(_ => parentNode.IsKind(_.Cs));
                if (pair != null)
                {
                    if (pair.Vb == CS.SyntaxKind.None)
                    {
                        return null;
                    }
                    bool flag = (CS.SyntaxFacts.IsExitStatement(pair.Vb));
                    return CS.SyntaxFactory.ExitStatement(pair.Vb, CS.SyntaxFactory.Token(pair.ExitTargetKeyword));
                }
                parentNode = parentNode.Parent;
            }
            System.Diagnostics.Debug.Assert(false, "break");
            return null;
        }

        static CSS.ContinueStatementSyntax ConvertContinue(this VBS.ContinueStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBreak = (VBS.ContinueStatementSyntax)node;

            SyntaxNode parentNode = node.Parent;
            while (parentNode != null)
            {
                var pair = ContinuePairs.FirstOrDefault(_ => parentNode.IsKind(_.Cs));
                if (pair != null)
                {
                    if (pair.Vb == CS.SyntaxKind.None)
                    {
                        return null;
                    }
                    bool flag = (CS.SyntaxFacts.IsExitStatement(pair.Vb));
                    return CS.SyntaxFactory.ContinueStatement(pair.Vb, CS.SyntaxFactory.Token(pair.ExitTargetKeyword));
                }
                parentNode = parentNode.Parent;
            }
            System.Diagnostics.Debug.Assert(false, "Continue");
            return null;

        }

        #region for
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <remarks>
        /// <code lang="CS">
        /// for(int i=0 ; i &lt; 10; i+=1)
        /// {
        ///     statementsA;
        ///     continue;
        ///     statementsB;
        /// }
        /// <code>
        /// <code lang="VB">
        /// With Nothing
        ///     Dim i=0
        ///     FOR _F_nnn As Integer = 0 TO 1 Step 0
        ///         If Not(i &lt; 10) Then
        ///             Exit For
        ///         End If
        ///         statementsA
        ///         Goto CONTINUE_FOR_nnn
        ///         statementsB
        ///     CONTINUE_FOR_nnn:
        ///         i+=1
        ///     Next
        /// End With  
        /// </code>
        /// </remarks>
        static CSS.WithBlockSyntax ConvertFor(this VBS.ForStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csFor = (VBS.ForStatementSyntax)node;
            var vbDeclaration = (CSS.DeclarationStatementSyntax)csFor.Declaration.Convert();
            var vbInitializers = csFor.Initializers.ConvertSyntaxNodes<CS.VisualBasicSyntaxNode>(); //?
            var vbCondition = (CSS.ExpressionSyntax)csFor.Condition.Convert();
            var vbIncrementors = csFor.Incrementors.ConvertSyntaxNodes<CS.VisualBasicSyntaxNode>().ToArray();
            var vbStatements = csFor.Statement.ConvertBlockOrStatements();

            uint number = ContinueForRewriter.CreateNumber();
            string forName = "_F_" + number.ToString();
            string gotoLabel = "CONTINUE_FOR_" + number.ToString();

            //Create For block //manual generating for this block is tiresome work...
            //手作業で作ると面倒なので
            var vbForBlock = (CSS.ForBlockSyntax)CS.SyntaxFactory.ParseExecutableStatement("For " + forName + " As Integer =0 To 1 Step 0");
            vbForBlock = vbForBlock.Update(vbForBlock.ForStatement, vbForBlock.Statements, CS.SyntaxFactory.NextStatement());
            //vbForBlock = vbForBlock.Update(vbForBlock.ForStatement, vbForBlock.Statements, VB.SyntaxFactory.NextStatement());

            //Insert loop end condition checker
            vbCondition = CS.SyntaxFactory.NotExpression(CS.SyntaxFactory.ParenthesizedExpression( vbCondition));
            var vbIf = CS.SyntaxFactory.MultiLineIfBlock(CS.SyntaxFactory.IfStatement(vbCondition));
            vbIf = vbIf.AddStatements(CS.SyntaxFactory.ExitForStatement());
            vbForBlock = vbForBlock.AddStatements(vbIf);

            //Add main statements
            vbForBlock = vbForBlock.AddStatements(vbStatements.ToArray());

            //'Continue For' will be replaced to jump to the end of block.
            ContinueForRewriter rewriter = new ContinueForRewriter(vbForBlock, gotoLabel);
            vbForBlock = (CSS.ForBlockSyntax)rewriter.Visit(vbForBlock);
            if (rewriter.HasContinueFor)
            {
                vbForBlock = vbForBlock.AddStatements(CS.SyntaxFactory.LabelStatement(rewriter.Label));
            }

            //Insert Post Incrementor Statements
            foreach (CS.VisualBasicSyntaxNode vbNode in vbIncrementors)
            {
                if (vbNode is CSS.ExpressionSyntax)
                {
                    var vbExp = (CSS.ExpressionSyntax)vbNode;
                    vbForBlock = vbForBlock.AddStatements(CS.SyntaxFactory.ExpressionStatement(vbExp));
                }
                else if (vbNode is CSS.StatementSyntax)
                {
                    vbForBlock = vbForBlock.AddStatements((CSS.StatementSyntax)vbNode);
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false, "ConvertFor");
                }
            }


            var vbWithNothing = CreateWithNothing();
            if (vbDeclaration != null)
            {
                //Insert Pre Declaration Statements
                if (vbDeclaration.Kind() == CS.SyntaxKind.FieldDeclaration)
                {
                    vbDeclaration = ((CSS.FieldDeclarationSyntax)vbDeclaration).AddModifiers(CS.SyntaxFactory.Token(CS.SyntaxKind.DimKeyword));
                }
                vbWithNothing = vbWithNothing.AddStatements(vbDeclaration);
            }
            vbWithNothing = vbWithNothing.AddStatements(vbForBlock);

            return vbWithNothing;
        }

        class ContinueForRewriter : CS.VisualBasicSyntaxRewriter
        {
            public static uint CreateNumber()
            {
                return (uint)Math.Abs(System.Threading.Interlocked.Increment(ref _ContinueForNumber));
            }
            private static int _ContinueForNumber;
            public static void ResetCounter()
            {
                _ContinueForNumber = 0;
            }

            public ContinueForRewriter(CSS.ForBlockSyntax vbFor, string gotoLabel)
            {
                if (vbFor == null) throw new ArgumentNullException("vbFor");

                _vbFor = vbFor;
                Label = gotoLabel;
            }
            private CSS.ForBlockSyntax _vbFor;
            public string Label { get; private set; }
            public bool HasContinueFor { get; private set; }

            public override SyntaxNode VisitContinueStatement(CSS.ContinueStatementSyntax node)
            {
                if (node.BlockKeyword.IsKind(CS.SyntaxKind.ForKeyword))
                {
                    SyntaxNode parent = node.Parent;
                    while (node != null)
                    {
                        if (parent.IsKind(CS.SyntaxKind.ForEachBlock))
                        {
                            break;
                        }
                        if (parent.IsKind(CS.SyntaxKind.ForBlock))
                        {
                            if (parent == _vbFor)
                            {
                                HasContinueFor = true;
                                var vbLabel = CS.SyntaxFactory.IdentifierLabel(Label);
                                return CS.SyntaxFactory.GoToStatement(vbLabel);
                            }
                            else
                            {
                            }
                            break;
                        }
                        parent = parent.Parent;
                    }
                }

                return base.VisitContinueStatement(node);

            }
        }

        #endregion //for

        static CSS.ForEachBlockSyntax ConvertForEach(this VBS.ForEachStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csForeach = (VBS.ForEachStatementSyntax)node;
            var vbID = csForeach.Identifier.ConvertID();

            CSS.SimpleAsClauseSyntax vbAsClause = null;
            if (!csForeach.Type.IsVar)
            {
                var vbType = (CSS.TypeSyntax)csForeach.Type.Convert();
                vbAsClause = CS.SyntaxFactory.SimpleAsClause(vbType);
            }
            var vbExpression = (CSS.ExpressionSyntax)csForeach.Expression.ConvertAssignRightExpression();
            var vbStatements = csForeach.Statement.ConvertBlockOrStatements();
            var vbMID = CS.SyntaxFactory.ModifiedIdentifier(vbID);


            var vbVariable = (CS.VisualBasicSyntaxNode)CS.SyntaxFactory.VariableDeclarator(vbMID).WithAsClause(vbAsClause);
            var vbForEachStatement = CS.SyntaxFactory.ForEachStatement(vbVariable, vbExpression);

            var vbForEach = CS.SyntaxFactory.ForEachBlock(vbForEachStatement)
                                .AddStatements(vbStatements.ToArray())
                                .WithNextStatement(CS.SyntaxFactory.NextStatement());

            return vbForEach;
        }

        static CSS.DoLoopBlockSyntax ConvertDo(this VBS.DoStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDo = (VBS.DoStatementSyntax)node;
            var vbStatements = csDo.Statement.ConvertBlockOrStatements();
            var vbCondition = (CSS.ExpressionSyntax)csDo.Condition.Convert();

            var vbWhileClause = CS.SyntaxFactory.WhileClause(vbCondition);
            var vbDoStatements = CS.SyntaxFactory.DoStatement(CS.SyntaxKind.SimpleDoStatement);
            var vbLoopStatement = CS.SyntaxFactory.LoopWhileStatement(vbWhileClause);

            var vbDo = CS.SyntaxFactory.DoLoopBlock(CS.SyntaxKind.DoLoopWhileBlock, vbDoStatements, vbLoopStatement);
            return vbDo.WithStatements(vbStatements);
        }

        static CSS.WhileBlockSyntax ConvertWhile(this VBS.WhileStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csWhile = (VBS.WhileStatementSyntax)node;
            var vbCondition = (CSS.ExpressionSyntax)csWhile.Condition.Convert();
            var vbStatements = csWhile.Statement.ConvertBlockOrStatements();

            var vbWhileStatements = CS.SyntaxFactory.WhileStatement(vbCondition);
            return CS.SyntaxFactory.WhileBlock(vbWhileStatements, vbStatements);
        }

        static CSS.GoToStatementSyntax ConvertGoto(this VBS.GotoStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csGoto = (VBS.GotoStatementSyntax)node;
            var vbID = (CSS.IdentifierNameSyntax)csGoto.Expression.ConvertAssignRightExpression();
            var vbLabel = CS.SyntaxFactory.IdentifierLabel(vbID.Identifier.ToFullString());
            return CS.SyntaxFactory.GoToStatement(vbLabel);
        }

        static CSS.LabelStatementSyntax ConvertLabel(this VBS.LabeledStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLabel = (VBS.LabeledStatementSyntax)node;
            var vbID = csLabel.Identifier.ConvertID();
            return CS.SyntaxFactory.LabelStatement(vbID.ToFullString());
        }

        static CSS.ReturnStatementSyntax ConvertReturn(this VBS.ReturnStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csReturn = (VBS.ReturnStatementSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csReturn.Expression.ConvertAssignRightExpression();
            return CS.SyntaxFactory.ReturnStatement(vbExpression);
        }

        static CSS.YieldStatementSyntax ConvertYield(this VBS.YieldStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csYieldReturn = (VBS.YieldStatementSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csYieldReturn.Expression.ConvertAssignRightExpression();
            return CS.SyntaxFactory.YieldStatement(vbExpression);
        }


        #endregion

        #region try-catch-finally

        static CSS.ThrowStatementSyntax ConvertThrow(this VBS.ThrowStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csThrow = (VBS.ThrowStatementSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csThrow.Expression.ConvertAssignRightExpression();
            return CS.SyntaxFactory.ThrowStatement(vbExpression);
        }

        static CSS.TryBlockSyntax ConvertTry(this VBS.TryStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTry = (VBS.TryStatementSyntax)node;
            var vbStatements = csTry.Block.Statements.ConvertSyntaxNodes<CSS.StatementSyntax>();
            var vbCatches = csTry.Catches.ConvertSyntaxNodes<CSS.CatchBlockSyntax>();
            var vbFinally = (CSS.FinallyBlockSyntax)csTry.Finally.Convert();

            var vbTry = CS.SyntaxFactory.TryBlock();
            vbTry = vbTry.WithStatements(vbStatements);
            vbTry = vbTry.AddCatchBlocks(vbCatches.ToArray());
            vbTry = vbTry.WithFinallyBlock(vbFinally);
            return vbTry;
        }
        static CSS.CatchBlockSyntax ConvertCatchClause(this VBS.CatchClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCatch = (VBS.CatchClauseSyntax)node;
            var vbFilter = (CSS.CatchFilterClauseSyntax)csCatch.Filter.Convert();
            var vbCatchStatement = (CSS.CatchStatementSyntax)csCatch.Declaration.Convert();
            var vbStatements = csCatch.Block.ConvertBlockOrStatements();
            if (vbFilter != null)
            {
                vbCatchStatement = vbCatchStatement.WithWhenClause(vbFilter);
            }
            if (vbCatchStatement == null)
            {
                vbCatchStatement = CS.SyntaxFactory.CatchStatement();
            }
            var vbCatchBlock = CS.SyntaxFactory.CatchBlock(vbCatchStatement);
            vbCatchBlock = vbCatchBlock.WithStatements(vbStatements);

            return vbCatchBlock;
        }
        static CSS.CatchStatementSyntax ConvertCatchDeclaration(this VBS.CatchDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCatchDeclaration = (VBS.CatchDeclarationSyntax)node;
            var vbType = (CSS.TypeSyntax)csCatchDeclaration.Type.Convert();
            var vbId = csCatchDeclaration.Identifier.ConvertID();
            var vbAsClause = CS.SyntaxFactory.SimpleAsClause(vbType);

            var vbCatchStatement = CS.SyntaxFactory.CatchStatement().WithAsClause(vbAsClause);
            if (vbId != null && !vbId.IsKind(CS.SyntaxKind.None))
            {
                var vbName = CS.SyntaxFactory.IdentifierName(vbId);
                vbCatchStatement = vbCatchStatement.WithIdentifierName(vbName);
            }
            else
            {
                var vbName = CS.SyntaxFactory.IdentifierName("ex");
                vbCatchStatement = vbCatchStatement.WithIdentifierName(vbName);
            }
            return vbCatchStatement;
        }
        static CSS.CatchFilterClauseSyntax ConvertCatchFilter(this VBS.CatchFilterClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCatchFilter = (VBS.CatchFilterClauseSyntax)node;
            var vbFilterExp = (CSS.ExpressionSyntax)csCatchFilter.FilterExpression.ConvertAssignRightExpression();
            return CS.SyntaxFactory.CatchFilterClause(vbFilterExp);
        }
        static CSS.FinallyBlockSyntax ConvertFinally(this VBS.FinallyClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csFinally = (VBS.FinallyClauseSyntax)node;
            var vbStatemetns = csFinally.Block.ConvertBlockOrStatements();
            return CS.SyntaxFactory.FinallyBlock(vbStatemetns);
        }

        #endregion

        #region field , variable

        static CSS.FieldDeclarationSyntax ConvertFieldDeclaration(this VBS.FieldDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csField = (VBS.FieldDeclarationSyntax)node;
            var vbAttribute = csField.AttributeLists.ConvertAttributes();
            var vbModifiers = ConvertModifiersWithDefault(csField.Modifiers, CS.SyntaxFactory.Token(CS.SyntaxKind.PrivateKeyword));

            var vbField = (CSS.FieldDeclarationSyntax)csField.Declaration.Convert();
            return vbField.AddAttributeLists(vbAttribute.ToArray()).AddModifiers(vbModifiers.ToArray());
        }

        static CSS.FieldDeclarationSyntax ConvertVariableDeclaration(this VBS.VariableDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDeclaration = (VBS.VariableDeclarationSyntax)node;
            var vbField = CS.SyntaxFactory.FieldDeclaration();
            var vbAsClause = csDeclaration.Type.ConvertToAsClause();

            foreach (VBS.VariableDeclaratorSyntax csv in csDeclaration.Variables)
            {
                var vbVariable = (CSS.VariableDeclaratorSyntax)csv.Convert();
                vbField = vbField.AddDeclarators(vbVariable.WithAsClause(vbAsClause));
            }

            return vbField;
        }

        static CSS.VariableDeclaratorSyntax ConvertVariableDeclarator(this VBS.VariableDeclaratorSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDeclarator = (VBS.VariableDeclaratorSyntax)node;
            var vbID = CS.SyntaxFactory.ModifiedIdentifier(csDeclarator.Identifier.ConvertID());
            var vbDeclarator = CS.SyntaxFactory.VariableDeclarator(vbID);
            var vbInitializer = (CSS.EqualsValueSyntax)csDeclarator.Initializer.Convert();

            return vbDeclarator.WithInitializer(vbInitializer);
        }

        static CSS.EnumMemberDeclarationSyntax ConvertEnumMemberDeclaration(this VBS.EnumMemberDeclarationSyntax node)
        {
            var csEnumDeclaration = (VBS.EnumMemberDeclarationSyntax)node;
            var vbAttributes = csEnumDeclaration.AttributeLists.ConvertAttributes();
            var vbEqualsValue = (CSS.EqualsValueSyntax)csEnumDeclaration.EqualsValue.Convert();
            var vbID = csEnumDeclaration.Identifier.ConvertID();

            return CS.SyntaxFactory.EnumMemberDeclaration(vbAttributes, vbID, vbEqualsValue);

        }

        static CSS.DelegateStatementSyntax ConvertDelegateDeclaration(this VBS.DelegateDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDelegate = (VBS.DelegateDeclarationSyntax)node;

            var vbAttributeListSplit = csDelegate.AttributeLists.ConvertAttributesSplit();
            var vbModifiers = ConvertModifiersWithDefault(csDelegate.Modifiers, GetDefaultModifiers(node));
            var vbIdentifier = csDelegate.Identifier.ConvertID();
            var vbTypeParameter = ConvertTypeParameters(csDelegate.TypeParameterList, csDelegate.ConstraintClauses);
            var vbParameters = (CSS.ParameterListSyntax)csDelegate.ParameterList.Convert();
            var vbReturn = (CSS.TypeSyntax)csDelegate.ReturnType.Convert();

            bool isSub = vbReturn == null;

            CS.SyntaxKind stateSubOrFunc = isSub ? CS.SyntaxKind.DelegateSubStatement : CS.SyntaxKind.DelegateFunctionStatement;
            CS.SyntaxKind keySubOrFunc = isSub ? CS.SyntaxKind.SubKeyword : CS.SyntaxKind.FunctionKeyword;
            var vbASClause = isSub ? null : CS.SyntaxFactory.SimpleAsClause(vbAttributeListSplit.Return, vbReturn);

            return CS.SyntaxFactory.DelegateStatement
                        (stateSubOrFunc
                        , vbAttributeListSplit.Others
                        , vbModifiers
                        , CS.SyntaxFactory.Token(keySubOrFunc)
                        , vbIdentifier
                        , vbTypeParameter
                        , vbParameters
                        , vbASClause);
        }

        static CSS.FieldDeclarationSyntax ConvertLocalDeclaration(this VBS.LocalDeclarationStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLocal = (VBS.LocalDeclarationStatementSyntax)node;
            var vbModifiers = ConvertModifiersWithDefault(csLocal.Modifiers, CS.SyntaxFactory.Token(CS.SyntaxKind.DimKeyword));
            var vbDeclaration = csLocal.Declaration.ConvertVariableDeclaration();
            return vbDeclaration.AddModifiers(vbModifiers.ToArray());
        }

        #endregion

        #region Event

        static CSS.EventStatementSyntax ConvertEventFieldDeclaration(this VBS.EventFieldDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csEvent = (VBS.EventFieldDeclarationSyntax)node;
            var vbAttributes = csEvent.AttributeLists.ConvertAttributes();
            var vbDeclaration = (CSS.FieldDeclarationSyntax)csEvent.Declaration.ConvertVariableDeclaration();
            var vbDeclarator = (CSS.VariableDeclaratorSyntax)vbDeclaration.Declarators[0];
            var vbModifiers = ConvertModifiersWithDefault(csEvent.Modifiers);

            var vbEvent = CS.SyntaxFactory.EventStatement(vbDeclarator.Names[0].GetText().ToString())
                               .WithAttributeLists(vbAttributes)
                               .WithModifiers(vbModifiers)
                               .WithAsClause((CSS.SimpleAsClauseSyntax)vbDeclarator.AsClause);
            return vbEvent;
        }

        static CSS.EventBlockSyntax ConvertEventBlock(this VBS.EventDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csEvent = (VBS.EventDeclarationSyntax)node;
            var vbAccessorList = csEvent.AccessorList.ConvertAccessorList();
            var vbAttributes = csEvent.AttributeLists.ConvertAttributes();
            var vbExplicit = (CSS.ImplementsClauseSyntax)csEvent.ExplicitInterfaceSpecifier.Convert();
            var vbID = csEvent.Identifier.ConvertID();
            var vbModifiers = ConvertModifiersWithDefault(csEvent.Modifiers);
            var vbAsClause = csEvent.Type.ConvertToAsClause();

            var vbRaiseArguments = CreateRaiseEventArguments(csEvent);
            var vbRaiseStatement = CS.SyntaxFactory.RaiseEventAccessorStatement();
            vbRaiseStatement = vbRaiseStatement.AddParameterListParameters(vbRaiseArguments.Parameters.ToArray());
            var vbRaiseAccessor = CS.SyntaxFactory.RaiseEventAccessorBlock(vbRaiseStatement);

            vbAccessorList = vbAccessorList.Add(vbRaiseAccessor);

            var vbEventStatement = CS.SyntaxFactory.EventStatement(vbID)
                                        .WithAttributeLists(vbAttributes)
                                        .WithCustomKeyword(CS.SyntaxFactory.Token(CS.SyntaxKind.CustomKeyword))
                                        .WithModifiers(vbModifiers)
                                        .WithAsClause(vbAsClause)
                                        .WithImplementsClause(vbExplicit);
            return CS.SyntaxFactory.EventBlock(vbEventStatement, vbAccessorList);
        }

        static CSS.AccessorBlockSyntax ConvertEventAccessor(this VBS.AccessorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csAddRemove = (VBS.AccessorDeclarationSyntax)node;
            var vbAttributes = csAddRemove.AttributeLists.ConvertAttributes();
            var vbModifiers = ConvertModifiersWithDefault(csAddRemove.Modifiers);
            var vbStatements = csAddRemove.Body.Statements.ConvertSyntaxNodes<CSS.StatementSyntax>();

            var csEvent = csAddRemove.Parent.Parent as VBS.EventDeclarationSyntax;

            var vbParameter = CS.SyntaxFactory.Parameter(CS.SyntaxFactory.ModifiedIdentifier("value"));
            vbParameter = vbParameter.AddModifiers(CS.SyntaxFactory.Token(CS.SyntaxKind.ByValKeyword));
            vbParameter = vbParameter.WithAsClause(csEvent.Type.ConvertToAsClause());
            var vbParameters = CS.SyntaxFactory.ParameterList().AddParameters(vbParameter);


            if (node.Kind() == VB.SyntaxKind.AddAccessorDeclaration)
            {
                var vbStatement = CS.SyntaxFactory.AddHandlerAccessorStatement(vbAttributes, vbModifiers, vbParameters);
                return CS.SyntaxFactory.AddHandlerAccessorBlock(vbStatement).WithStatements(vbStatements);
            }
            else
            {
                var vbStatement = CS.SyntaxFactory.RemoveHandlerAccessorStatement(vbAttributes, vbModifiers, vbParameters);
                return CS.SyntaxFactory.RemoveHandlerAccessorBlock(vbStatement).WithStatements(vbStatements);
            }
        }

        private static CSS.ParameterListSyntax CreateRaiseEventArguments(VBS.EventDeclarationSyntax csEvent)
        {

            var vbRaiseArguments = CS.SyntaxFactory.ParameterList();
            string handlerTypeName = "";
            if (csEvent.Type.Kind() == VB.SyntaxKind.IdentifierName)
            {
                var csType = (VBS.IdentifierNameSyntax)csEvent.Type;
                handlerTypeName = csType.Identifier.Text;

                if (handlerTypeName == "EventHandler")
                {
                    vbRaiseArguments = vbRaiseArguments.AddParameters(CreateRaiseParameter("sender", "Object"));
                    vbRaiseArguments = vbRaiseArguments.AddParameters(CreateRaiseParameter("e", "EventArgs"));
                }
                else if (handlerTypeName != "Action")
                {
                    //not support Custom delegate;
                    //I don't know how to get the arguments of unknown delegate.
                    //未知のデリゲートの引数の調べ方が不明です
                }
            }
            else if (csEvent.Type.Kind() == VB.SyntaxKind.GenericName)
            {
                var csGeneric = ((VBS.GenericNameSyntax)csEvent.Type);
                handlerTypeName = csGeneric.Identifier.Text;
                if (handlerTypeName == "Action"
                    || (handlerTypeName == "EventHandler" && csGeneric.TypeArgumentList.Arguments.Count == 1))
                {
                    if (handlerTypeName == "EventHandler")
                    {
                        vbRaiseArguments = vbRaiseArguments.AddParameters(CreateRaiseParameter("sender", "Object"));
                    }
                    int i = 0;
                    foreach (VBS.TypeSyntax csType in csGeneric.TypeArgumentList.Arguments)
                    {
                        var vbParameterAsClause = csType.ConvertToAsClause();
                        var vbParameter = CS.SyntaxFactory.Parameter(CS.SyntaxFactory.ModifiedIdentifier("arg" + i.ToString()));
                        vbParameter = vbParameter.AddModifiers(CS.SyntaxFactory.Token(CS.SyntaxKind.ByValKeyword));
                        vbParameter = vbParameter.WithAsClause(vbParameterAsClause);
                        vbRaiseArguments = vbRaiseArguments.AddParameters(vbParameter);
                    }
                }
                else
                {
                    //not suppoort Custom delegate;
                    //I don't know how to get the arguments of unknown delegate.
                    //未知のデリゲートの引数の調べ方が不明です
                }
            }
            else //if (csEvent.Type.Kind() == CS.SyntaxKind.QualifiedName)
            {
                System.Diagnostics.Debug.Assert(false, "CreateRaiseEventArguments");
            }
            return vbRaiseArguments;
        }
        private static CSS.ParameterSyntax CreateRaiseParameter(string argumentName, string typename)
        {
            var vbEventArgs = CS.SyntaxFactory.IdentifierName(typename);
            var vbAsEventArgs = CS.SyntaxFactory.SimpleAsClause(vbEventArgs);
            var vbParameter = CS.SyntaxFactory.Parameter(CS.SyntaxFactory.ModifiedIdentifier(argumentName));
            vbParameter = vbParameter.AddModifiers(CS.SyntaxFactory.Token(CS.SyntaxKind.ByValKeyword));
            return vbParameter.WithAsClause(vbAsEventArgs);
        }

        #endregion

        #region Property

        static CSS.PropertyBlockSyntax ConvertIndexer(this VBS.IndexerDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csIndexer = (VBS.IndexerDeclarationSyntax)node;
            var vbAttributes = csIndexer.AttributeLists.ConvertAttributes();
            var vbExplicitInterface = csIndexer.ExplicitInterfaceSpecifier.ConvertExplicitInterfaceSpecifier();
            var vbExpression = csIndexer.ExpressionBody.Convert();

            var vbModifiers = ConvertModifiers(csIndexer.Modifiers);
            var vbAsClause = csIndexer.Type.ConvertToAsClause();
            var vbAccessors = csIndexer.AccessorList.ConvertAccessorList();
            CSS.AccessorBlockSyntax vbGetBlock = vbAccessors.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.GetAccessorBlock));
            CSS.AccessorBlockSyntax vbSetBlock = vbAccessors.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.SetAccessorBlock));

            var vbParameters = (CSS.ParameterListSyntax)csIndexer.ParameterList.Convert();
            SyntaxToken vbID = CS.SyntaxFactory.Identifier(VB.SyntaxFactory.Token(VB.SyntaxKind.ThisKeyword).ToFullString());
            CSS.EqualsValueSyntax vbInitializer = null;
            bool isReadOnly = vbGetBlock != null && vbSetBlock == null;
            bool isWriteOnly = vbGetBlock == null && vbSetBlock != null;
            if (isReadOnly)
            {
                vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.ReadOnlyKeyword));
            }
            else if (isWriteOnly)
            {
                vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.WriteOnlyKeyword));
            }
            vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.DefaultKeyword));
            var vbPropertyStatement = CS.SyntaxFactory.PropertyStatement(vbAttributes, vbModifiers, vbID, vbParameters, vbAsClause, vbInitializer, vbExplicitInterface);

            return CS.SyntaxFactory.PropertyBlock(vbPropertyStatement, vbAccessors);
        }

        static CS.VisualBasicSyntaxNode ConvertProperty(this VBS.PropertyDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csPropertyDeclaration = (VBS.PropertyDeclarationSyntax)node;

            var vbAttributes = csPropertyDeclaration.AttributeLists.ConvertAttributes();
            var vbExplicitInterface = csPropertyDeclaration.ExplicitInterfaceSpecifier.ConvertExplicitInterfaceSpecifier();
            var vbExpression = csPropertyDeclaration.ExpressionBody.Convert();
            var vbID = csPropertyDeclaration.Identifier.ConvertID();
            var vbInitializer = (CSS.EqualsValueSyntax)csPropertyDeclaration.Initializer.Convert();
            var vbModifiers = ConvertModifiers(csPropertyDeclaration.Modifiers);
            var vbAsClause = csPropertyDeclaration.Type.ConvertToAsClause();
            var vbAccessors = csPropertyDeclaration.AccessorList.ConvertAccessorList();
            CSS.AccessorBlockSyntax vbGetBlock = vbAccessors.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.GetAccessorBlock));
            CSS.AccessorBlockSyntax vbSetBlock = vbAccessors.FirstOrDefault(_ => _.IsKind(CS.SyntaxKind.SetAccessorBlock));

            bool isReadOnly = vbGetBlock != null && vbSetBlock == null;
            bool isWriteOnly = vbGetBlock == null && vbSetBlock != null;
            if (isReadOnly)
            {
                vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.ReadOnlyKeyword));
            }
            else if (isWriteOnly)
            {
                vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.WriteOnlyKeyword));
            }

            bool isAutoProperty = null != csPropertyDeclaration.AccessorList.Accessors.FirstOrDefault(_ => _.Body == null);
            bool isNeedBackingField = isAutoProperty & vbAccessors.Count(_ => _.AccessorStatement.Modifiers.Count != 0) != 0;
            isAutoProperty &= !isNeedBackingField;

            var vbPropertyStatement = CS.SyntaxFactory.PropertyStatement(vbAttributes, vbModifiers, vbID, null, vbAsClause, vbInitializer, vbExplicitInterface);

            if (csPropertyDeclaration.Modifiers.Count(_ => _.IsKind(VB.SyntaxKind.AbstractKeyword)) > 0)
            {
                return vbPropertyStatement;
            }

            if (isAutoProperty)
            {
                return vbPropertyStatement;
            }
            else
            {
                return CS.SyntaxFactory.PropertyBlock(vbPropertyStatement, vbAccessors);
            }
        }

        static CSS.AccessorBlockSyntax ConvertGetSet(this VBS.AccessorDeclarationSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csAccessor = (VBS.AccessorDeclarationSyntax)node;
            var vbAttributes = csAccessor.AttributeLists.ConvertAttributes();
            var vbModifiers = ConvertModifiers(csAccessor.Modifiers);
            var vbStatements = csAccessor.Body.ConvertBlockOrStatements();
            if (csAccessor.Kind() == VB.SyntaxKind.GetAccessorDeclaration)
            {
                var vbAccessorStatement = CS.SyntaxFactory.GetAccessorStatement(vbAttributes, vbModifiers, null);
                return CS.SyntaxFactory.GetAccessorBlock(vbAccessorStatement, vbStatements);
            }
            else
            {
                var vbAccessorStatement = CS.SyntaxFactory.SetAccessorStatement(vbAttributes, vbModifiers, null);
                return CS.SyntaxFactory.SetAccessorBlock(vbAccessorStatement, vbStatements);
            }
        }

        #endregion

        #endregion

        #region Attribute

        static SyntaxList<CSS.AttributeListSyntax> ConvertAttributes(this SyntaxList<VBS.AttributesStatementSyntax> attributes)
        {
            return attributes.Convert<VBS.AttributesStatementSyntax, CSS.AttributeListSyntax>();
        }

        static AttributeSplitResult ConvertAttributesSplit(this SyntaxList<VBS.AttributeListSyntax> attributes)
        {
            AttributeSplitResult ret = new AttributeSplitResult();

            foreach (VBS.AttributeListSyntax csAttributeList in attributes)
            {
                var vbAttributeList = (CSS.AttributeListSyntax)csAttributeList.Convert();

                bool isReturnTarget = csAttributeList.Target != null && csAttributeList.Target.Identifier.ValueText == "return";
                if (isReturnTarget)
                {
                    ret.Return = ret.Return.Add(vbAttributeList);
                }
                else
                {
                    ret.Others = ret.Others.Add(vbAttributeList);
                }
            }
            return ret;
        }

        class AttributeSplitResult
        {
            public SyntaxList<CSS.AttributeListSyntax> Others = new SyntaxList<CSS.AttributeListSyntax>();
            public SyntaxList<CSS.AttributeListSyntax> Return = new SyntaxList<CSS.AttributeListSyntax>();
        }

        static SyntaxList<CSS.AttributeSyntax> ConvertAttributes(this SeparatedSyntaxList<VBS.AttributeSyntax> attributes)
        {
            return attributes.Convert<VBS.AttributeSyntax, CSS.AttributeSyntax>();
        }

        static SyntaxList<CSS.AccessorBlockSyntax> ConvertAccessorList(this VBS.AccessorListSyntax csAccessors)
        {
            return csAccessors.Accessors.ConvertSyntaxNodes<CSS.AccessorBlockSyntax>();
        }

        #endregion

        #region Type

        static CSS.TypeParameterListSyntax ConvertTypeParameterList(this VBS.TypeParameterListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeParameterList = (VBS.TypeParameterListSyntax)node;
            var vbTypeParameters = csTypeParameterList.Parameters.ConvertSyntaxNodes<CSS.TypeParameterSyntax>();
            return CS.SyntaxFactory.TypeParameterList(vbTypeParameters.ToArray());
        }

        static CSS.TypeParameterSyntax ConvertTypeParameter(this VBS.TypeParameterSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeParameter = (VBS.TypeParameterSyntax)node;
            return CS.SyntaxFactory.TypeParameter(csTypeParameter.Identifier.ValueText); ;
        }

        static CSS.InheritsStatementSyntax ConvertBaseList(this VBS.BaseListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBaseList = (VBS.BaseListSyntax)node;
            var vbTypes = csBaseList.Types.ConvertSyntaxNodes<CSS.TypeSyntax>();
            return CS.SyntaxFactory.InheritsStatement(vbTypes.ToArray());
        }

        static CSS.TypeSyntax ConvertSimpleBaseType(this VBS.SimpleBaseTypeSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csType = (VBS.SimpleBaseTypeSyntax)node;
            return (CSS.TypeSyntax)csType.Type.Convert();
        }

        static CSS.TypeArgumentListSyntax ConvertTypeArgumentList(this VBS.TypeArgumentListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeArgumentList = (VBS.TypeArgumentListSyntax)node;
            var vbTypes = csTypeArgumentList.Arguments.ConvertSyntaxNodes<CSS.TypeSyntax>();
            return CS.SyntaxFactory.TypeArgumentList(vbTypes.ToArray());
        }

        static CSS.PredefinedTypeSyntax ConvertPredefinedTypeType(this VBS.PredefinedTypeSyntax csType)
        {
            foreach (KeywordPair pair in TypePairs)
            {
                if (csType.Keyword.IsKind(pair.Cs))
                {
                    return CS.SyntaxFactory.PredefinedType(CS.SyntaxFactory.Token(pair.Vb));
                }
            }
            return null;
        }

        static CSS.ArrayTypeSyntax ConvertArrayType(this VBS.ArrayTypeSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArray = (VBS.ArrayTypeSyntax)node;
            var vbElementType = (CSS.TypeSyntax)csArray.ElementType.Convert();
            var vbRanks = csArray.RankSpecifiers.ConvertSyntaxNodes<CSS.ArrayRankSpecifierSyntax>();
            return CS.SyntaxFactory.ArrayType(vbElementType, vbRanks);
        }

        static CSS.ArrayRankSpecifierSyntax ConvertArrayRank(this VBS.ArrayRankSpecifierSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArrayRank = (VBS.ArrayRankSpecifierSyntax)node;

            int length = csArrayRank.Rank - 1;
            List<SyntaxToken> tokens = new List<SyntaxToken>();
            for (int i = 0; i < length; i++)
            {
                tokens.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.CommaToken));
            }
            var vbArrayRank = CS.SyntaxFactory.ArrayRankSpecifier();
            return vbArrayRank.AddCommaTokens(tokens.ToArray());
        }

        static CSS.NullableTypeSyntax ConvertNullableType(this VBS.NullableTypeSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csNullType = (VBS.NullableTypeSyntax)node;
            var vbElementType = (CSS.PredefinedTypeSyntax)csNullType.ElementType.Convert();
            return CS.SyntaxFactory.NullableType(vbElementType);
        }

        #endregion

        #region Parameter

        static CSS.ParameterListSyntax ConvertParameterList(this VBS.ParameterListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csParameters = (VBS.ParameterListSyntax)node;
            var vbParameters = csParameters.Parameters.ConvertSyntaxNodes<CSS.ParameterSyntax>();
            return CS.SyntaxFactory.ParameterList().AddParameters(vbParameters.ToArray());
        }

        static CSS.ParameterSyntax ConvertParameter(this VBS.ParameterSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csParameter = (VBS.ParameterSyntax)node;
            var vbParamName = CS.SyntaxFactory.ModifiedIdentifier(csParameter.Identifier.ConvertID());
            var vbParameter = CS.SyntaxFactory.Parameter(vbParamName);

            SyntaxTokenList mods = new SyntaxTokenList();
            if (csParameter.Default != null)
            {
                mods = mods.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.OptionalKeyword));
            }
            if (csParameter.Modifiers.Count == 0)
            {
                mods = mods.Add(CS.SyntaxFactory.Token(Microsoft.CodeAnalysis.VisualBasic.SyntaxKind.ByValKeyword));
            }
            else
            {
                bool hasByref = false;
                bool hasParams = false;

                if (csParameter.Default != null)
                {
                    mods = mods.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.OptionalKeyword));
                }

                foreach (SyntaxToken token in csParameter.Modifiers)
                {
                    if (!hasByref && (token.IsKind(VB.SyntaxKind.RefKeyword) || token.IsKind(VB.SyntaxKind.OutKeyword)))
                    {
                        mods = mods.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.ByRefKeyword));
                        hasByref = true;
                    }
                    else if (!hasParams && token.IsKind(VB.SyntaxKind.ParamsKeyword))
                    {
                        mods = mods.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.ParamArrayKeyword));
                        hasParams = true;
                    }
                }
                if (!hasByref)
                {
                    mods = mods.Add(CS.SyntaxFactory.Token(Microsoft.CodeAnalysis.VisualBasic.SyntaxKind.ByValKeyword));
                }
            }

            if (mods.Count > 0)
            {
                vbParameter = vbParameter.WithModifiers(mods);
            }

            var vbAsClause = csParameter.Type.ConvertToAsClause();
            vbParameter = vbParameter.WithAsClause(vbAsClause);

            if (csParameter.Default != null)
            {
                var vbExpSyntax = (CSS.ExpressionSyntax)csParameter.Default.Value.Convert();
                var vbEQValue = CS.SyntaxFactory.EqualsValue(vbExpSyntax);

                vbParameter = vbParameter.WithDefault(vbEQValue);
            }
            return vbParameter;
        }

        static CSS.TypeParameterConstraintClauseSyntax ConvertTypeParameterConstraintClauseSyntax(this VBS.TypeParameterConstraintClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeParameterConstraintClause = (VBS.TypeParameterConstraintClauseSyntax)node;
            var vbTypeParameterConstraints = csTypeParameterConstraintClause.Constraints.ConvertSyntaxNodes<CSS.ConstraintSyntax>();// <VB.VisualBasicSyntaxNode>();
            if (vbTypeParameterConstraints.Count == 1)
            {
                return CS.SyntaxFactory.TypeParameterSingleConstraintClause(vbTypeParameterConstraints.First());
            }
            else
            {
                return CS.SyntaxFactory.TypeParameterMultipleConstraintClause(vbTypeParameterConstraints.ToArray());
            }
        }
        //case CS.SyntaxKind.ClassConstraint:
        //    return VB.SyntaxFactory.ClassConstraint(VB.SyntaxFactory.Token(VB.SyntaxKind.ClassKeyword));
        //case CS.SyntaxKind.StructConstraint:
        //    return VB.SyntaxFactory.StructureConstraint(VB.SyntaxFactory.Token(VB.SyntaxKind.StructureKeyword));
        //case CS.SyntaxKind.ConstructorConstraint:
        //    return VB.SyntaxFactory.NewConstraint(VB.SyntaxFactory.Token(VB.SyntaxKind.NewKeyword));

        static CSS.TypeConstraintSyntax ConvertTypeParameterConstraint(this VBS.TypeParameterConstraintSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeParameterConsraint = (VBS.TypeParameterConstraintSyntax)node;
            var csTypeConstraint = (VBS.TypeConstraintSyntax)node;
            var vbType = (CSS.TypeSyntax)csTypeConstraint.Type.Convert();
            return CS.SyntaxFactory.TypeConstraint(vbType);
        }

        #endregion

        #region Name

        static CSS.TypeSyntax ConvertIdentifierName(this VBS.IdentifierNameSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            VBS.IdentifierNameSyntax csID = (VBS.IdentifierNameSyntax)node;
            if (csID.Identifier.ValueText == "dynamic")
            {
                return CS.SyntaxFactory.PredefinedType(CS.SyntaxFactory.Token(CS.SyntaxKind.ObjectKeyword));
            }
            return CS.SyntaxFactory.IdentifierName(csID.Identifier.ValueText);
        }

        static CSS.GenericNameSyntax ConvertGenericName(this VBS.GenericNameSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csGenericName = (VBS.GenericNameSyntax)node;
            var vbID = csGenericName.Identifier.ConvertID();
            var vbTypeArgumentList = (CSS.TypeArgumentListSyntax)csGenericName.TypeArgumentList.Convert();
            return CS.SyntaxFactory.GenericName(vbID, vbTypeArgumentList);
        }
        static CSS.QualifiedNameSyntax ConvertQualifiedName(this VBS.QualifiedNameSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csQualified = (VBS.QualifiedNameSyntax)node;
            var vbNameLeft = (CSS.NameSyntax)csQualified.Left.Convert();
            var vbNameRight = (CSS.SimpleNameSyntax)csQualified.Right.Convert();
            return CS.SyntaxFactory.QualifiedName(vbNameLeft, vbNameRight);
        }

        static CSS.QualifiedNameSyntax ConvertAliasQualifiedName(this VBS.AliasQualifiedNameSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csAliasQualified = (VBS.AliasQualifiedNameSyntax)node;
            var vbAlias = (CSS.IdentifierNameSyntax)csAliasQualified.Alias.Convert();
            var vbName = (CSS.SimpleNameSyntax)csAliasQualified.Name.Convert();
            return CS.SyntaxFactory.QualifiedName(vbAlias, vbName);
        }


        #endregion

        #region Field,Literal

        static SyntaxToken ConvertID(this SyntaxToken csID)
        {
            if (csID.IsKind(VB.SyntaxKind.IdentifierToken))
            {
                string id = csID.ValueText;
                if (!(csID.Parent is VBS.MethodDeclarationSyntax) && IsVBKeyword(id))
                {
                    return CS.SyntaxFactory.BracketedIdentifier(id);

                }
                else
                {
                    return CS.SyntaxFactory.Identifier(id);
                }
            }
            return default(SyntaxToken);
        }

        static CSS.SimpleAsClauseSyntax ConvertToAsClause(this VBS.TypeSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csType = (VBS.TypeSyntax)node;
            if (csType == null || csType.IsVar)
            {
                return null;
            }
            else
            {
                var vbType = (CSS.TypeSyntax)csType.Convert();
                if (vbType == null)
                {
                    return null;
                }
                return CS.SyntaxFactory.SimpleAsClause(vbType);
            }
        }

        static CSS.LiteralExpressionSyntax ConvertLiteral(this VBS.LiteralExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLiteral = (VBS.LiteralExpressionSyntax)node;
            if (csLiteral.IsKind(VB.SyntaxKind.NullLiteralExpression))
            {
                return CS.SyntaxFactory.NothingLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.NothingKeyword));
            }

            string sValue = csLiteral.Token.ValueText;
            SyntaxToken vbToken = default(SyntaxToken);
            if (csLiteral.IsKind(VB.SyntaxKind.StringLiteralExpression))
            {
                vbToken = CS.SyntaxFactory.Token(CS.SyntaxKind.StringKeyword, sValue);
                return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.StringLiteralExpression, vbToken);
            }

            if (csLiteral.IsKind(VB.SyntaxKind.NumericLiteralExpression))
            {

                var match = System.Text.RegularExpressions.Regex.Match(csLiteral.Token.Text, "[DFLMU]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string vbSuffix = "";
                    switch (match.Value.ToUpper())
                    {
                    case "L": vbSuffix = "L"; break;
                    case "U":
                        {
                            if (csLiteral.Token.Value is uint)
                            {
                                vbSuffix = "UI"; break;
                            }
                            else
                            {
                                vbSuffix = "UL"; break;
                            }
                        }
                    case "LU":
                    case "UL": vbSuffix = "UL"; break;
                    case "F": vbSuffix = "F"; break;
                    case "D": vbSuffix = "R"; break;
                    case "M": vbSuffix = "D"; break;
                    }
                    sValue = csLiteral.Token.Text.Substring(0, csLiteral.Token.Text.Length - match.Value.Length) + vbSuffix;
                }

                if (sValue.StartsWith("0x"))
                {
                    sValue = "&H" + sValue.Substring(2);
                }

                if (csLiteral.Token.Value is sbyte
                    || csLiteral.Token.Value is short
                    || csLiteral.Token.Value is int
                    || csLiteral.Token.Value is long)
                {
                    long l = ((IConvertible)csLiteral.Token.Value).ToInt64(null);
                    ulong ul = (ulong)Math.Abs(l);
                    vbToken = CS.SyntaxFactory.IntegerLiteralToken(sValue, CSS.LiteralBase.Decimal, CSS.TypeCharacter.Integer, ul);
                    if (l < 0)
                    {
                        vbToken = CS.SyntaxFactory.Token(CS.SyntaxKind.MinusToken, "-");
                    }
                    return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.NumericLiteralExpression, vbToken);

                }
                else if (csLiteral.Token.Value is byte
                     || csLiteral.Token.Value is ushort
                     || csLiteral.Token.Value is uint
                     || csLiteral.Token.Value is ulong)
                {
                    ulong ul = ((IConvertible)csLiteral.Token.Value).ToUInt64(null);
                    vbToken = CS.SyntaxFactory.IntegerLiteralToken(sValue, CSS.LiteralBase.Decimal, CSS.TypeCharacter.Integer, ul);
                    return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.NumericLiteralExpression, vbToken);
                }
                else if (csLiteral.Token.Value is float || csLiteral.Token.Value is double)
                {
                    var dbl = ((IConvertible)csLiteral.Token.Value).ToDouble(null);
                    vbToken = CS.SyntaxFactory.FloatingLiteralToken(sValue, CSS.TypeCharacter.Single, dbl);
                    return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.NumericLiteralExpression, vbToken);
                }
                else if (csLiteral.Token.Value is Decimal)
                {
                    vbToken = CS.SyntaxFactory.DecimalLiteralToken(sValue, CSS.TypeCharacter.Decimal, (Decimal)csLiteral.Token.Value);
                    return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.NumericLiteralExpression, vbToken);
                }
            }

            System.Diagnostics.Debug.Assert(false, "ConvertLiteral");
            return default(CSS.LiteralExpressionSyntax);
        }

        static CSS.ExpressionSyntax ConvertControlChar(char c)
        {
            if (Char.IsControl(c))
            {
                for (int i = 0; i < ControlCharPairs.GetLength(0); i++)
                {
                    if (ControlCharPairs[i, 0][0] == c)
                    {
                        var vbToken = CS.SyntaxFactory.CharacterLiteralToken(ControlCharPairs[i, 1], c);
                        return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.CharacterLiteralExpression, vbToken);
                    }
                }

                return CS.SyntaxFactory.ParseExpression("ChrW(" + ((int)c).ToString() + ")");
            }
            else
            {
                var vbToken = CS.SyntaxFactory.CharacterLiteralToken(c.ToString(), c);
                return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.CharacterLiteralExpression, vbToken);
            }
        }

        static CSS.ExpressionSyntax ConvertLiteralChar(this VBS.LiteralExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLiteral = (VBS.LiteralExpressionSyntax)node;
            char c = (char)csLiteral.Token.Value;
            string s = "\"" + c.ToString() + "\"c";
            if (Char.IsControl(c))
            {
                return ConvertControlChar(c);
            }
            else
            {
                var vbToken = CS.SyntaxFactory.CharacterLiteralToken(s, c);
                return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.CharacterLiteralExpression, vbToken);
            }
        }

        static CSS.ExpressionSyntax ConvertLiteralString(string text, string value, bool appendBracket = true)
        {
            if (appendBracket)
            {
                text = "\"" + text + "\"";
            }
            var vbToken = CS.SyntaxFactory.Literal(text, value);
            return CS.SyntaxFactory.LiteralExpression(CS.SyntaxKind.StringLiteralExpression, vbToken);
        }

        static CSS.ExpressionSyntax ConvertLiteralString(this VBS.LiteralExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLiteral = (VBS.LiteralExpressionSyntax)node;

            string text = (string)csLiteral.Token.Value;
            if (text == null)
            {
                return CS.SyntaxFactory.NothingLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.NothingKeyword));
            }
            else if (text.Length == 0)
            {
                return ConvertLiteralString("", "");
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<CSS.ExpressionSyntax> list = new List<CSS.ExpressionSyntax>();
            bool isPreviewCR = false;
            foreach (char c in text)
            {
                if (Char.IsControl(c))
                {
                    if (sb.Length > 0)
                    {
                        string valueText = sb.ToString();
                        list.Add(ConvertLiteralString(valueText.Replace("\"", "\"\""), valueText));
                        sb.Clear();
                    }

                    if (isPreviewCR && c == '\n')
                    {
                        list[list.Count - 1] = ConvertLiteralString("vbCrlf", "\r\n", false);
                    }
                    else
                    {
                        list.Add(ConvertControlChar(c));
                    }
                    isPreviewCR = c == '\r';
                }
                else
                {
                    sb.Append(c);
                    isPreviewCR = false;
                }
            }
            if (sb.Length > 0)
            {
                string part = sb.ToString();
                list.Add(ConvertLiteralString(part.Replace("\"", "\"\""), part));
            }

            if (list.Count == 1)
            {
                return list[0];
            }
            else
            {
                //  expression[0] + expression[1] + expression[2] + ...
                var expression = CS.SyntaxFactory.AddExpression(list[0], list[1]);
                for (int i = 2; i < list.Count; i++)
                {
                    expression = CS.SyntaxFactory.AddExpression(expression, list[i]);
                }
                return expression;
            }

        }


        #endregion

        #region InterpolatedString
        static CSS.InterpolatedStringExpressionSyntax ConvertInterpolatedStringExpression(this VBS.InterpolatedStringExpressionSyntax node)
        {
            List<CSS.InterpolatedStringContentSyntax> list = new List<CSS.InterpolatedStringContentSyntax>();
            var csInterpolatedStringExpressinon = (VBS.InterpolatedStringExpressionSyntax)node;
            foreach (VBS.InterpolatedStringContentSyntax csInterpolatedString in csInterpolatedStringExpressinon.Contents)
            {
                if (csInterpolatedString.Kind() == VB.SyntaxKind.InterpolatedStringText)
                {
                    var csInterpolatedText = (VBS.InterpolatedStringTextSyntax)csInterpolatedString;
                    string valueText = csInterpolatedText.TextToken.ValueText;
                    list.AddRange(SplitInterpolatedStringText(valueText));
                }
                else
                {
                    list.Add((CSS.InterpolatedStringContentSyntax)csInterpolatedString.Convert());
                }
            }
            return CS.SyntaxFactory.InterpolatedStringExpression().AddContents(list.ToArray());
        }
        private static CSS.InterpolatedStringTextSyntax CreateVBInterpolatedString(string valueTextWithoutControlChar)
        {
            string text = valueTextWithoutControlChar.Replace("\"", "\"\"");
            var vbToken = CS.SyntaxFactory.InterpolatedStringTextToken(text, valueTextWithoutControlChar);
            return CS.SyntaxFactory.InterpolatedStringText(vbToken);
        }
        private static CSS.InterpolationSyntax GetVBCRInterpolation()
        {
            var vbCRLF = CS.SyntaxFactory.IdentifierName("vbCrLf");
            return CS.SyntaxFactory.Interpolation(vbCRLF);
        }
        private static CSS.InterpolationSyntax CreateControlCharInterpolation(char c)
        {
            var vbCharW = ConvertControlChar(c);
            return CS.SyntaxFactory.Interpolation(vbCharW);
        }

        static CSS.InterpolationAlignmentClauseSyntax ConvertInterpolationAlignmentClause(this VBS.InterpolationAlignmentClauseSyntax node)
        {
            var csAlignment = (VBS.InterpolationAlignmentClauseSyntax)node;
            var vbExprettion = (CSS.ExpressionSyntax)csAlignment.Value.Convert();
            return CS.SyntaxFactory.InterpolationAlignmentClause(vbExprettion);
        }

        static CSS.InterpolatedStringContentSyntax ConvertInterpolatedStringText(this VBS.InterpolatedStringTextSyntax node)
        {
            var csInterpolatedText = (VBS.InterpolatedStringTextSyntax)node;
            string text = csInterpolatedText.TextToken.Text;
            string valueText = csInterpolatedText.TextToken.ValueText;
            if (valueText.Count(c => Char.IsControl(c)) == 0)
            {
                var vbToken = CS.SyntaxFactory.InterpolatedStringTextToken(text, valueText);
                return CS.SyntaxFactory.InterpolatedStringText(vbToken);
            }
            else
            {
                var list = SplitInterpolatedStringText(valueText);
                var vbInterpolatedStringExpression = CS.SyntaxFactory.InterpolatedStringExpression().AddContents(list.ToArray());
                return CS.SyntaxFactory.Interpolation(vbInterpolatedStringExpression);
            }
        }

        private static List<CSS.InterpolatedStringContentSyntax> SplitInterpolatedStringText(string valueText)
        {
            List<CSS.InterpolatedStringContentSyntax> list = new List<CSS.InterpolatedStringContentSyntax>();
            bool isPreviewCR = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char c in valueText)
            {
                if (Char.IsControl(c))
                {
                    if (sb.Length > 0)
                    {
                        list.Add(CreateVBInterpolatedString(sb.ToString()));
                        sb.Clear();
                    }

                    if (isPreviewCR && c == '\n')
                    {
                        list[list.Count - 1] = GetVBCRInterpolation();
                    }
                    else
                    {
                        list.Add(CreateControlCharInterpolation(c));
                    }
                    isPreviewCR = c == '\r';
                }
                else
                {
                    sb.Append(c);
                    isPreviewCR = false;
                }
            }
            if (sb.Length > 0)
            {
                list.Add(CreateVBInterpolatedString(sb.ToString()));
                sb.Clear();
            }
            return list;
        }

        static CSS.InterpolationSyntax ConvertInterpolation(this VBS.InterpolationSyntax node)
        {
            var csInterpolation = (VBS.InterpolationSyntax)node;
            var vbAlignment = (CSS.InterpolationAlignmentClauseSyntax)csInterpolation.AlignmentClause.Convert();
            var vbExpression = (CSS.ExpressionSyntax)csInterpolation.Expression.ConvertAssignRightExpression();
            var vbFormat = (CSS.InterpolationFormatClauseSyntax)csInterpolation.FormatClause.Convert();

            return CS.SyntaxFactory.Interpolation(vbExpression, vbAlignment).WithFormatClause(vbFormat); ;
        }

        static CSS.InterpolationFormatClauseSyntax ConvertInterpolationFormatClause(this VBS.InterpolationFormatClauseSyntax node)
        {
            var csInterpolationFormat = (VBS.InterpolationFormatClauseSyntax)node;
            var vbToken = CS.SyntaxFactory.InterpolatedStringTextToken(csInterpolationFormat.FormatStringToken.Text, csInterpolationFormat.FormatStringToken.ValueText);
            return CS.SyntaxFactory.InterpolationFormatClause(CS.SyntaxFactory.Token(CS.SyntaxKind.ColonToken), vbToken);
        }

        #endregion


        #region Expression

        static CSS.StatementSyntax ConvertExpressionStatement(this VBS.ExpressionStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csExpS = (VBS.ExpressionStatementSyntax)node;
            CSS.StatementSyntax vbStatement;
            object vb = csExpS.Expression.Convert();
            if (vb == null)
            {
                return null;
            }
            else if (vb is CSS.ExpressionSyntax)
            {
                var vbSyntax = (CSS.ExpressionSyntax)vb;
                vbStatement = CS.SyntaxFactory.ExpressionStatement(vbSyntax);
            }
            else
            {
                vbStatement = vb as CSS.StatementSyntax;
            }

            return vbStatement;
        }

        static CSS.CTypeExpressionSyntax ConvertDefaultExpression(this VBS.DefaultExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csDefault = (VBS.DefaultExpressionSyntax)node;
            var vbType = (CSS.TypeSyntax)csDefault.Type.Convert();
            var vbNothing = CS.SyntaxFactory.NothingLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.NothingKeyword));
            return CS.SyntaxFactory.CTypeExpression(vbNothing, vbType);
        }

        static CSS.DirectCastExpressionSyntax ConvertCast(this VBS.CastExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCast = (VBS.CastExpressionSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csCast.Expression.ConvertAssignRightExpression();
            var vbType = (CSS.TypeSyntax)csCast.Type.Convert();
            return CS.SyntaxFactory.DirectCastExpression(vbExpression, vbType);
        }

        static CSS.TryCastExpressionSyntax ConvertAs(this VBS.BinaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBinaryExp = (VBS.BinaryExpressionSyntax)node;
            var vbLeft = (CSS.ExpressionSyntax)csBinaryExp.Left.Convert();
            var vbRight = (CSS.ExpressionSyntax)csBinaryExp.Right.Convert();
            return CS.SyntaxFactory.TryCastExpression(vbLeft, (CSS.TypeSyntax)vbRight);
        }

        private static CSS.ExpressionSyntax ConvertAssignRightExpression(this VBS.ExpressionSyntax csRight)
        {
            if (csRight is VBS.AssignmentExpressionSyntax)
            {
                //C#
                //  int a; int b;
                //  a = b = 1;
                //     ^^^^^^^ AssignmentExpressionSyntax
                //
                //VB
                //  Dim a as Integer
                //  Dim b as Integer 
                //  a = Func()
                //         b = 1
                //         return b  
                //      End Function()
                var vbFunctionHeader = CS.SyntaxFactory.FunctionLambdaHeader().WithParameterList(CS.SyntaxFactory.ParameterList());
                var vbRight = (CSS.AssignmentStatementSyntax)csRight.Convert();
                var vbReturn = CS.SyntaxFactory.ReturnStatement(vbRight.Left);
                var vbEndFunction = CS.SyntaxFactory.EndFunctionStatement();
                var vbFunction = CS.SyntaxFactory.MultiLineFunctionLambdaExpression(vbFunctionHeader, vbEndFunction);

                vbFunction = vbFunction.AddStatements(vbRight).AddStatements(vbReturn);
                var vbBlankArgument=CS.SyntaxFactory.ArgumentList();
                return CS.SyntaxFactory.InvocationExpression(vbFunction, vbBlankArgument);
            }
            else
            {
                return (CSS.ExpressionSyntax)csRight.Convert();
            }
        }

        static CSS.StatementSyntax ConvertSimpleAssignment(this VBS.AssignmentExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csAssignment = (VBS.AssignmentExpressionSyntax)node;
            var vbLeft = (CSS.ExpressionSyntax)csAssignment.Left.Convert();
            var vbRight = (CSS.ExpressionSyntax)csAssignment.Right.ConvertAssignRightExpression();
            return CS.SyntaxFactory.SimpleAssignmentStatement(vbLeft, vbRight);
        }



        static CSS.AssignmentStatementSyntax ConvertAssignmentExpression(VBS.AssignmentExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csAssignmentExpression = (VBS.AssignmentExpressionSyntax)node;
            var vbLeft = (CSS.ExpressionSyntax)csAssignmentExpression.Left.Convert();
            var vbRight = (CSS.ExpressionSyntax)csAssignmentExpression.Right.ConvertAssignRightExpression();

            switch (csAssignmentExpression.Kind())
            {
            case VB.SyntaxKind.AddAssignmentExpression:// +=
                //Event Assign is Not Support
                return CS.SyntaxFactory.AddAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.SubtractAssignmentExpression: // -=
                //Event Assign is Not Support
                return CS.SyntaxFactory.SubtractAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.MultiplyAssignmentExpression: // *=
                return CS.SyntaxFactory.MultiplyAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.DivideAssignmentExpression: // /=
                return CS.SyntaxFactory.DivideAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.LeftShiftAssignmentExpression: // <<=
                return CS.SyntaxFactory.LeftShiftAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.RightShiftAssignmentExpression: // >>=
                return CS.SyntaxFactory.RightShiftAssignmentStatement(vbLeft, vbRight);

            case VB.SyntaxKind.ModuloAssignmentExpression:// ^=
                vbRight = CS.SyntaxFactory.ModuloExpression(vbLeft, vbRight);
                return CS.SyntaxFactory.SimpleAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.AndAssignmentExpression:// &=
                vbRight = CS.SyntaxFactory.AndExpression(vbLeft, vbRight);
                return CS.SyntaxFactory.SimpleAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.OrAssignmentExpression:// |=
                vbRight = CS.SyntaxFactory.OrExpression(vbLeft, vbRight);
                return CS.SyntaxFactory.SimpleAssignmentStatement(vbLeft, vbRight);
            case VB.SyntaxKind.ExclusiveOrAssignmentExpression:// ^=
                vbRight = CS.SyntaxFactory.ExclusiveOrExpression(vbLeft, vbRight);
                return CS.SyntaxFactory.SimpleAssignmentStatement(vbLeft, vbRight);
            }
            return null;
        }

        static CS.VisualBasicSyntaxNode ConvertNameEquals(this VBS.NameEqualsSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csNameEqual = (VBS.NameEqualsSyntax)node;
            var vbName = (CSS.IdentifierNameSyntax)csNameEqual.Name.Convert();
            if (csNameEqual.Parent.IsKind(VB.SyntaxKind.UsingDirective))
            {//Using ***=###; alias
                return CS.SyntaxFactory.ImportAliasClause(vbName.ToFullString());
            }
            else if (csNameEqual.Parent.IsKind(VB.SyntaxKind.AttributeArgument))
            {//[DllImport("user32.dll", CharSet = CharSet.Auto)]
                return CS.SyntaxFactory.NameColonEquals(vbName);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, node.Kind().ToString());
            }
            return null;
        }

        static CSS.EqualsValueSyntax ConvertEqualsValue(this VBS.EqualsValueClauseSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csEqualsValue = (VBS.EqualsValueClauseSyntax)node;
            var vbValue = (CSS.ExpressionSyntax)csEqualsValue.Value.Convert();
            return CS.SyntaxFactory.EqualsValue(vbValue);
        }

        static CSS.ObjectCreationExpressionSyntax ConvertObjectCreation(this VBS.ObjectCreationExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csCreation = (VBS.ObjectCreationExpressionSyntax)node;
            var vbType = (CSS.TypeSyntax)csCreation.Type.Convert();
            var vbArgument = (CSS.ArgumentListSyntax)csCreation.ArgumentList.Convert();
            var vbCreation = CS.SyntaxFactory.ObjectCreationExpression(vbType);
            vbCreation = vbCreation.AddArgumentListArguments(vbArgument.Arguments.ToArray());
            return vbCreation;
        }

        static CSS.CollectionInitializerSyntax ConvertArrayInitializer(this VBS.InitializerExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csInitializer = (VBS.InitializerExpressionSyntax)node;
            var vbInitializer = CS.SyntaxFactory.CollectionInitializer();
            foreach (VBS.ExpressionSyntax e in csInitializer.Expressions)
            {
                var vbExpression = (CSS.ExpressionSyntax)e.Convert();
                if (vbExpression.Kind() == CS.SyntaxKind.CollectionInitializer)
                {
                    SyntaxNode parent = node.Parent;
                    CSS.TypeSyntax vbType = null;
                    while (parent != null)
                    {
                        if (parent.IsKind(VB.SyntaxKind.ArrayCreationExpression))
                        {
                            var csArrayCreation = (VBS.ArrayCreationExpressionSyntax)parent;
                            vbType = (CSS.TypeSyntax)csArrayCreation.Type.Convert();
                            break;
                        }
                        else if (parent.IsKind(VB.SyntaxKind.VariableDeclaration))
                        {
                            var csVariable = (VBS.VariableDeclarationSyntax)parent;
                            vbType = (CSS.TypeSyntax)csVariable.Type.Convert();
                            break;
                        }
                        parent = parent.Parent;
                    }
                    if (vbType != null && vbType.Kind() == CS.SyntaxKind.ArrayType)
                    {
                        var vbAtype = (CSS.ArrayTypeSyntax)vbType;

                        if (vbAtype.RankSpecifiers.Count >= 2)
                        {
                            //Jagged Array
                            vbType = ((CSS.ArrayTypeSyntax)vbType).ElementType;
                            vbExpression = CS.SyntaxFactory.ArrayCreationExpression(vbType, (CSS.CollectionInitializerSyntax)vbExpression)
                                                .WithArrayBounds(CS.SyntaxFactory.ArgumentList());
                        }

                    }
                }
                vbInitializer = vbInitializer.AddInitializers(vbExpression);
            }
            return vbInitializer;
        }

        static CSS.ArrayCreationExpressionSyntax ConvertArrayCreation(this VBS.ArrayCreationExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArrayCreation = (VBS.ArrayCreationExpressionSyntax)node;
            var csAType = (VBS.ArrayTypeSyntax)csArrayCreation.Type;
            var vbAType = (CSS.ArrayTypeSyntax)csAType.Convert();
            var vbInitializer = (CSS.CollectionInitializerSyntax)csArrayCreation.Initializer.Convert();
            if (vbInitializer == null)
            {
                vbInitializer = CS.SyntaxFactory.CollectionInitializer();
            }

            var vbArgs = new SyntaxList<CSS.ArgumentSyntax>();
            if (csAType.RankSpecifiers.Count > 0 && csAType.RankSpecifiers[0].Sizes != null)
            {
                bool hasInitializer = vbInitializer.Initializers.Count > 0;
                foreach (VBS.ExpressionSyntax csExp in csAType.RankSpecifiers[0].Sizes)
                {
                    if (hasInitializer || csExp.Kind() == VB.SyntaxKind.OmittedArraySizeExpression)
                    {
                        vbArgs = vbArgs.Add(CS.SyntaxFactory.OmittedArgument());
                    }
                    else
                    {
                        var vbExp = (CSS.ExpressionSyntax)csExp.Convert();
                        vbArgs = vbArgs.Add(CS.SyntaxFactory.SimpleArgument(vbExp));
                    }
                }
            }

            var vbArray = CS.SyntaxFactory.ArrayCreationExpression(vbAType.ElementType, vbInitializer)
                                .AddArrayBoundsArguments(vbArgs.ToArray());

            for (int i = 1; i < vbAType.RankSpecifiers.Count; i++)
            {//Jagged Array
                vbArray = vbArray.AddRankSpecifiers(CS.SyntaxFactory.ArrayRankSpecifier());
            }
            return vbArray;
        }

        static CSS.ArgumentListSyntax ConvertBaeArgumentList(this VBS.BaseArgumentListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArguments = (VBS.BaseArgumentListSyntax)node;
            var vbArgs = csArguments.Arguments.ConvertSyntaxNodes<CSS.ArgumentSyntax>();
            return CS.SyntaxFactory.ArgumentList().AddArguments(vbArgs.ToArray());
        }


        static CSS.ParameterListSyntax ConvertBracketedParameter(this VBS.BracketedParameterListSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArguments = (VBS.BracketedParameterListSyntax)node;
            var vbParameters = csArguments.Parameters.ConvertSyntaxNodes<CSS.ParameterSyntax>();
            return CS.SyntaxFactory.ParameterList().AddParameters(vbParameters.ToArray());
        }

        static CSS.SimpleArgumentSyntax ConvertArgument(this VBS.ArgumentSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csArg = (VBS.ArgumentSyntax)node;
            var vbExp = (CSS.ExpressionSyntax)csArg.Expression.ConvertAssignRightExpression();
            var vv = csArg.NameColon.Convert();
            var vbArg = CS.SyntaxFactory.SimpleArgument(vbExp);
            return vbArg;
        }

        static CSS.InvocationExpressionSyntax ConvertInvocationExpretion(this VBS.InvocationExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csInvocation = (VBS.InvocationExpressionSyntax)node;
            var vbExp = (CSS.ExpressionSyntax)csInvocation.Expression.ConvertAssignRightExpression();
            var vbArguments = (CSS.ArgumentListSyntax)csInvocation.ArgumentList.Convert();

            return CS.SyntaxFactory.InvocationExpression(vbExp, vbArguments);
        }

        static CSS.MemberAccessExpressionSyntax ConvertMemberAccess(this VBS.MemberAccessExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csMemberAccess = (VBS.MemberAccessExpressionSyntax)node;

            var vbExp = (CSS.ExpressionSyntax)csMemberAccess.Expression.ConvertAssignRightExpression();
            var vbName = (CSS.SimpleNameSyntax)csMemberAccess.Name.Convert();
            SyntaxToken vbOperator = default(SyntaxToken);
            if (csMemberAccess.OperatorToken.IsKind(VB.SyntaxKind.DotToken))
            {
                vbOperator = CS.SyntaxFactory.Token(CS.SyntaxKind.DotToken);
            }
            else if (csMemberAccess.OperatorToken.IsKind(VB.SyntaxKind.ColonColonToken))
            {
                vbOperator = CS.SyntaxFactory.Token(CS.SyntaxKind.DotToken);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "SimpleMemberAccessExpression");
            }

            var vbMemberAccess = CS.SyntaxFactory.MemberAccessExpression(CS.SyntaxKind.SimpleMemberAccessExpression, vbOperator, vbName);
            vbMemberAccess = vbMemberAccess.WithExpression(vbExp);

            return vbMemberAccess;
        }

        static CSS.InvocationExpressionSyntax ConvertElementAccess(this VBS.ElementAccessExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csElementAccess = (VBS.ElementAccessExpressionSyntax)node;
            var vbExp = (CSS.ExpressionSyntax)csElementAccess.Expression.ConvertAssignRightExpression();
            var vbArgs = (CSS.ArgumentListSyntax)csElementAccess.ArgumentList.Convert();
            var vbElementAccess = CS.SyntaxFactory.InvocationExpression(vbExp, vbArgs);
            return vbElementAccess;
        }

        static CSS.ParenthesizedExpressionSyntax ConvertParenthesized(this VBS.ParenthesizedExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csParenthesized = (VBS.ParenthesizedExpressionSyntax)node;
            var vbExp = (CSS.ExpressionSyntax)csParenthesized.Expression.ConvertAssignRightExpression();
            return CS.SyntaxFactory.ParenthesizedExpression(vbExp);
        }


        static CSS.LiteralExpressionSyntax ConvertNullLiteral()
        {
            var nothing = CS.SyntaxFactory.Token(CS.SyntaxKind.NothingKeyword);
            return CS.SyntaxFactory.NothingLiteralExpression(nothing);
        }
        static CSS.LiteralExpressionSyntax ConvertTrueLiteral()
        {
            return CS.SyntaxFactory.TrueLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.TrueKeyword));
        }
        static CSS.LiteralExpressionSyntax ConvertFalseLiteral()
        {
            return CS.SyntaxFactory.FalseLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.FalseKeyword));
        }

        static CSS.UnaryExpressionSyntax ConvertUnraryPlus(this VBS.PrefixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csUnary = (VBS.PrefixUnaryExpressionSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csUnary.Operand.Convert();
            return CS.SyntaxFactory.UnaryPlusExpression(vbExpression);
        }
        static CSS.UnaryExpressionSyntax ConvertUnraryMinus(this VBS.PrefixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csUnary = (VBS.PrefixUnaryExpressionSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csUnary.Operand.Convert();
            return CS.SyntaxFactory.UnaryMinusExpression(vbExpression);
        }

        static CSS.GetTypeExpressionSyntax ConvertTypeOf(this VBS.TypeOfExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csTypeExp = (VBS.TypeOfExpressionSyntax)node;
            return CS.SyntaxFactory.GetTypeExpression((CSS.TypeSyntax)csTypeExp.Type.Convert());
        }

        static CS.VisualBasicSyntaxNode ConvertBinaryExpression(VBS.BinaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csBinaryExp = (VBS.BinaryExpressionSyntax)node;
            var vbLeft = (CSS.ExpressionSyntax)csBinaryExp.Left.Convert();
            var vbRight = (CSS.ExpressionSyntax)csBinaryExp.Right.Convert();

            switch (node.Kind())
            {
            case VB.SyntaxKind.IsExpression:
                if (vbRight is CSS.TypeSyntax)
                {
                    return CS.SyntaxFactory.TypeOfIsExpression(vbLeft, (CSS.TypeSyntax)vbRight);
                }
                else
                {
                    return CS.SyntaxFactory.IsExpression(vbLeft, vbRight);
                }

            case VB.SyntaxKind.AddExpression:// +
                return CS.SyntaxFactory.AddExpression(vbLeft, vbRight);
            case VB.SyntaxKind.SubtractExpression:// -
                return CS.SyntaxFactory.DivideExpression(vbLeft, vbRight);
            case VB.SyntaxKind.MultiplyExpression:// *
                return CS.SyntaxFactory.MultiplyExpression(vbLeft, vbRight);
            case VB.SyntaxKind.DivideExpression:// /
                return CS.SyntaxFactory.DivideExpression(vbLeft, vbRight);
            case VB.SyntaxKind.ModuloExpression:// %
                return CS.SyntaxFactory.ModuloExpression(vbLeft, vbRight);

            case VB.SyntaxKind.LeftShiftExpression: // <<
                return CS.SyntaxFactory.LeftShiftExpression(vbLeft, vbRight);
            case VB.SyntaxKind.RightShiftExpression: // >>
                return CS.SyntaxFactory.RightShiftExpression(vbLeft, vbRight);

            case VB.SyntaxKind.BitwiseAndExpression: // &
                return CS.SyntaxFactory.AndExpression(vbLeft, vbRight);
            case VB.SyntaxKind.BitwiseOrExpression: // |
                return CS.SyntaxFactory.OrExpression(vbLeft, vbRight);
            case VB.SyntaxKind.ExclusiveOrExpression: // ^
                return CS.SyntaxFactory.ExclusiveOrExpression(vbLeft, vbRight);

            case VB.SyntaxKind.LogicalAndExpression: // &&
                return CS.SyntaxFactory.AndAlsoExpression(vbLeft, vbRight);
            case VB.SyntaxKind.LogicalOrExpression: // ||
                return CS.SyntaxFactory.OrElseExpression(vbLeft, vbRight);

            case VB.SyntaxKind.EqualsExpression: // == 
                if (vbRight.IsKind(CS.SyntaxKind.NothingLiteralExpression))
                {
                    return CS.SyntaxFactory.IsExpression(vbLeft, vbRight);
                }
                else
                {
                    return CS.SyntaxFactory.EqualsExpression(vbLeft, vbRight);
                }

            case VB.SyntaxKind.NotEqualsExpression: // != 
                if (vbRight.IsKind(CS.SyntaxKind.NothingLiteralExpression))
                {
                    return CS.SyntaxFactory.IsNotExpression(vbLeft, vbRight);
                }
                else
                {
                    return CS.SyntaxFactory.NotEqualsExpression(vbLeft, vbRight);
                }

            case VB.SyntaxKind.GreaterThanExpression: // >
                return CS.SyntaxFactory.GreaterThanExpression(vbLeft, vbRight);
            case VB.SyntaxKind.GreaterThanOrEqualExpression: // >
                return CS.SyntaxFactory.GreaterThanOrEqualExpression(vbLeft, vbRight);
            case VB.SyntaxKind.LessThanExpression: // <
                return CS.SyntaxFactory.LessThanExpression(vbLeft, vbRight);
            case VB.SyntaxKind.LessThanOrEqualExpression: // <=
                return CS.SyntaxFactory.LessThanOrEqualExpression(vbLeft, vbRight);

            case VB.SyntaxKind.CoalesceExpression: // ??
                return CS.SyntaxFactory.BinaryConditionalExpression(vbLeft, vbRight);
            }

            System.Diagnostics.Debug.Assert(false, "Operator Expression");
            return null;
        }

        static CSS.UnaryExpressionSyntax ConvertNot(this VBS.PrefixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var cs = (VBS.PrefixUnaryExpressionSyntax)node;
            var vbExp = (CSS.ExpressionSyntax)cs.Operand.Convert();
            var vbParenthesizedExpression = CS.SyntaxFactory.ParenthesizedExpression(vbExp);
            return CS.SyntaxFactory.NotExpression(vbParenthesizedExpression);
        }

        private static CSS.ExpressionSyntax CreateInvocation(string methodName, VBS.ExpressionSyntax csArg)
        {
            var vbExpression = (CSS.ExpressionSyntax)csArg.Convert();
            var vbArg = CS.SyntaxFactory.SimpleArgument(vbExpression);
            var vbArgs = CS.SyntaxFactory.ArgumentList().AddArguments(vbArg);
            var vbID = CS.SyntaxFactory.IdentifierName(methodName);
            return CS.SyntaxFactory.InvocationExpression(vbID, vbArgs);
        }

        static CSS.ExpressionSyntax ConvertPreIncrement(this VBS.PrefixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            return CreateInvocation("__PreIncrement__", ((VBS.PrefixUnaryExpressionSyntax)node).Operand);
        }
        static CSS.ExpressionSyntax ConvertPreDecrement(this VBS.PrefixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            return CreateInvocation("__PreDecrement__", ((VBS.PrefixUnaryExpressionSyntax)node).Operand);
        }
        static CSS.ExpressionSyntax ConvertPostIncrement(this VBS.PostfixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            return CreateInvocation("__PostIncrement__", ((VBS.PostfixUnaryExpressionSyntax)node).Operand);
        }
        static CSS.ExpressionSyntax ConvertPostDecrement(this VBS.PostfixUnaryExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            return CreateInvocation("__PostDecrement__", ((VBS.PostfixUnaryExpressionSyntax)node).Operand);
        }

        static CSS.TernaryConditionalExpressionSyntax ConvertConditional(this VBS.ConditionalExpressionSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csConditional = (VBS.ConditionalExpressionSyntax)node;
            var vbCondition = (CSS.ExpressionSyntax)csConditional.Condition.Convert();
            var vbLeftValue = (CSS.ExpressionSyntax)csConditional.WhenTrue.Convert();
            var vbRightValue = (CSS.ExpressionSyntax)csConditional.WhenFalse.Convert();

            return CS.SyntaxFactory.TernaryConditionalExpression(vbCondition, vbLeftValue, vbRightValue);
        }

        static SeparatedSyntaxList<CSS.ExpressionSyntax> ConvertExpressions(this SeparatedSyntaxList<VBS.ExpressionSyntax> csExps)
        {
            SeparatedSyntaxList<CSS.ExpressionSyntax> retval = new SeparatedSyntaxList<CSS.ExpressionSyntax>();
            var vbExps = csExps.Select(csExp => csExp.Convert() as CSS.ExpressionSyntax).Where(vbExp => vbExp != null);
            return retval.AddRange(vbExps);
        }

        #endregion



        #region Other

        static CSS.ImplementsClauseSyntax ConvertExplicitInterfaceSpecifier(this VBS.ExplicitInterfaceSpecifierSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csExplicitInterface = (VBS.ExplicitInterfaceSpecifierSyntax)node;

            var csParent = (VBS.MemberDeclarationSyntax)csExplicitInterface.Parent;
            SyntaxToken csID;
            if (csExplicitInterface.Parent.IsKind(VB.SyntaxKind.MethodDeclaration))
            {
                var csMethod = (VBS.MethodDeclarationSyntax)csExplicitInterface.Parent;
                csID = csMethod.Identifier;
            }
            else if (csExplicitInterface.Parent.IsKind(VB.SyntaxKind.PropertyDeclaration))
            {
                var csProperty = (VBS.PropertyDeclarationSyntax)csExplicitInterface.Parent;
                csID = csProperty.Identifier;
            }
            else if (csExplicitInterface.Parent.IsKind(VB.SyntaxKind.EventDeclaration))
            {
                var csEvent = (VBS.EventDeclarationSyntax)csExplicitInterface.Parent;
                csID = csEvent.Identifier;
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "ExplicitInterfaceSpecifier");
                return null;
            }
            var vbMemberName = csID.ConvertID();
            var vbID = CS.SyntaxFactory.IdentifierName(csID.ValueText);
            var vbInterfaceName = (CSS.NameSyntax)csExplicitInterface.Name.Convert();
            var vbInterfaceMemberName = CS.SyntaxFactory.QualifiedName(vbInterfaceName, vbID);

            return CS.SyntaxFactory.ImplementsClause(vbInterfaceMemberName);
        }

        static CSS.UsingBlockSyntax ConvertUsingBlock(this VBS.UsingStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csUsing = (VBS.UsingStatementSyntax)node;
            var vbDeclaration = (CSS.DeclarationStatementSyntax)csUsing.Declaration.Convert();
            var vbExpression = (CSS.ExpressionSyntax)csUsing.Expression.ConvertAssignRightExpression();
            var vbStatements = csUsing.Statement.ConvertBlockOrStatements();

            var vbUsingStatement = CS.SyntaxFactory.UsingStatement();
            if (vbDeclaration is CSS.FieldDeclarationSyntax)
            {
                var vbField = (CSS.FieldDeclarationSyntax)vbDeclaration;
                vbUsingStatement = vbUsingStatement.AddVariables(vbField.Declarators.ToArray());
            }
            else if (vbExpression != null)
            {
                vbUsingStatement = vbUsingStatement.WithExpression(vbExpression);

            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "UsingStatement");
            }

            return CS.SyntaxFactory.UsingBlock(vbUsingStatement).AddStatements(vbStatements.ToArray());
        }

        static SyntaxList<CSS.StatementSyntax> ConvertBlockStatements(this VBS.BlockSyntax node)
        {
            if (node == null)
            {
                return new SyntaxList<CSS.StatementSyntax>();
            }
            return node.Statements.ConvertStatements<CSS.StatementSyntax>();// <CSS.StatementSyntax, VBS.StatementSyntax>();
        }

        static CSS.SyncLockBlockSyntax ConvertLock(this VBS.LockStatementSyntax node)
        {
            if (node == null)
            {
                return null;
            }
            var csLock = (VBS.LockStatementSyntax)node;
            var vbExpression = (CSS.ExpressionSyntax)csLock.Expression.ConvertAssignRightExpression();
            var vbStatements = csLock.Statement.ConvertBlockOrStatements();

            var vbSyncLockStatement = CS.SyntaxFactory.SyncLockStatement(vbExpression);
            return CS.SyntaxFactory.SyncLockBlock(vbSyncLockStatement).AddStatements(vbStatements.ToArray());
        }

        static SyntaxList<CSS.StatementSyntax> ConvertBlockOrStatements(this VBS.StatementSyntax node)
        {
            if (node == null)
            {
                return new SyntaxList<CSS.StatementSyntax>();
            }

            if (node is VBS.BlockSyntax)
            {
                var csBlock = (VBS.BlockSyntax)node;
                return csBlock.Statements.Convert<VBS.StatementSyntax, CSS.StatementSyntax>();
            }
            else if (node is VBS.StatementSyntax)
            {
                var vbStatement = (CSS.StatementSyntax)node.Convert();
                var vbStatements = new SyntaxList<CSS.StatementSyntax>();
                if (vbStatement == null)
                {
                    return vbStatements;
                }

                return vbStatements.Add(vbStatement);
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "ConvertBlockStatments");
                return new SyntaxList<CSS.StatementSyntax>(); ;
            }
        }

        static SyntaxList<CSS.StatementSyntax> ConvertMembers(this IEnumerable<VBS.MemberDeclarationSyntax> csMembers) //where T : CSS.MemberDeclarationSyntax
        {
            SyntaxList<CSS.StatementSyntax> retval = new SyntaxList<CSS.StatementSyntax>();
            var vbmembers = csMembers.Convert<VBS.MemberDeclarationSyntax, CSS.StatementSyntax>().Where(b => b != null);
            return retval.AddRange(vbmembers);
        }

        static SyntaxList<CSS.StatementSyntax> ConvertClassMembers(this VBS.ClassDeclarationSyntax csClass) //where T : CSS.MemberDeclarationSyntax
        {
            var csMembers = csClass.Members;
            SyntaxList<CSS.StatementSyntax> retval = new SyntaxList<CSS.StatementSyntax>();

            List<SyntaxToken> csIDs = GetCSMemberIdentifier(csMembers);
            List<string> vbIDs = csIDs.Select(_ => _.ConvertID().ToFullString()).ToList();

            foreach (VBS.MemberDeclarationSyntax csMember in csMembers)
            {
                if (csMember.Kind() == VB.SyntaxKind.PropertyDeclaration)
                {
                    var csProperty = (VBS.PropertyDeclarationSyntax)csMember;
                    bool hasGetter = false;
                    bool hasSetter = false;
                    VBS.AccessorDeclarationSyntax csGetter = null;
                    VBS.AccessorDeclarationSyntax csSetter = null;
                    foreach (VBS.AccessorDeclarationSyntax csAccessor in csProperty.AccessorList.Accessors)
                    {
                        if (csAccessor.Body == null)
                        {
                        }
                        if (csAccessor.Kind() == VB.SyntaxKind.GetAccessorDeclaration)
                        {
                            hasGetter = true;
                            csGetter = csAccessor;
                        }
                        else if (csAccessor.Kind() == VB.SyntaxKind.SetAccessorDeclaration)
                        {
                            hasSetter = true;
                            csSetter = csAccessor;
                        }
                    }
                    if ((hasGetter && csGetter.Body != null) || (hasSetter && csSetter.Body != null))
                    {
                        //Not Auto Property
                        retval = retval.Add((CSS.StatementSyntax)csMember.Convert());
                    }
                    else if (hasGetter && hasSetter && csGetter.Body == null && csSetter.Body == null && csGetter.Modifiers.Count == 0 && csSetter.Modifiers.Count == 0)
                    {
                        //Auto Property
                        retval = retval.Add((CSS.StatementSyntax)csMember.Convert());
                    }
                    else if ((hasGetter && csGetter.Body == null) || (hasSetter && csSetter.Body == null))
                    {
                        //Expand Backing field;

                        var vbIDString = "_" + ((VBS.PropertyDeclarationSyntax)csMember).Identifier.ConvertID().ToFullString();
                        int count = 2;
                        while (vbIDs.Contains(vbIDString) || IsVBKeyword(vbIDString))
                        {
                            vbIDString = vbIDString + "_" + count.ToString();
                            count++;
                        }
                        vbIDs.Add(vbIDString);
                        var vbMID = CS.SyntaxFactory.ModifiedIdentifier(vbIDString);
                        var csID = VB.SyntaxFactory.IdentifierName(vbIDString);

                        var csInitializer = csProperty.Initializer;
                        csProperty = csProperty.WithInitializer(null);

                        var csAccessors = VB.SyntaxFactory.AccessorList();
                        if (hasGetter)
                        {
                            var csGetterBlock = VB.SyntaxFactory.Block();
                            csGetterBlock = csGetterBlock.AddStatements(VB.SyntaxFactory.ReturnStatement(csID));
                            csGetter = csGetter.WithBody(csGetterBlock);
                            csAccessors = csAccessors.AddAccessors(csGetter);
                        }

                        if (hasSetter)
                        {
                            var csSetterBlock = VB.SyntaxFactory.Block();
                            var csLeft = csID;
                            var csRight = VB.SyntaxFactory.IdentifierName("value");
                            var csAssign = VB.SyntaxFactory.AssignmentExpression(VB.SyntaxKind.SimpleAssignmentExpression, csLeft, csRight);
                            csSetterBlock = csSetterBlock.AddStatements(VB.SyntaxFactory.ExpressionStatement(csAssign));
                            csSetter = csSetter.WithBody(csSetterBlock);
                            csAccessors = csAccessors.AddAccessors(csSetter);
                        }
                        csProperty = csProperty.WithAccessorList(csAccessors);

                        CS.VisualBasicSyntaxNode vbPropety = csProperty.Convert();
                        CSS.PropertyStatementSyntax vbPropertyStatement;
                        if (vbPropety.IsKind(CS.SyntaxKind.PropertyStatement))
                        {
                            vbPropertyStatement = (CSS.PropertyStatementSyntax)vbPropety;
                            retval = retval.Add(vbPropertyStatement);
                        }
                        else
                        {
                            var vbPropertyBlock = (CSS.PropertyBlockSyntax)vbPropety;
                            vbPropertyStatement = vbPropertyBlock.PropertyStatement;

                            retval = retval.Add(vbPropertyBlock);
                        }

                        SyntaxTokenList vbModifiers = new SyntaxTokenList();
                        vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.PrivateKeyword));
                        if (vbPropertyStatement.Modifiers.Count(_ => _.IsKind(CS.SyntaxKind.SharedKeyword)) > 0)
                        {
                            vbModifiers = vbModifiers.Add(CS.SyntaxFactory.Token(CS.SyntaxKind.SharedKeyword));
                        }

                        var vbVariable = CS.SyntaxFactory.VariableDeclarator(vbMID);
                        vbVariable = vbVariable.WithAsClause(vbPropertyStatement.AsClause);
                        if (csInitializer != null)
                        {
                            var vbInitializer = (CSS.EqualsValueSyntax)csInitializer.Convert();
                            vbVariable = vbVariable.WithInitializer(vbInitializer);
                        }

                        var vbBackingField = CS.SyntaxFactory.FieldDeclaration(vbVariable).WithModifiers(vbModifiers);

                        retval = retval.Add(vbBackingField);
                    }
                }
                else
                {
                    var vbStatement = (CSS.StatementSyntax)csMember.Convert();
                    if (vbStatement == null)
                    {
                    }
                    else
                    {
                        retval = retval.Add((CSS.StatementSyntax)csMember.Convert());
                    }
                }
            }
            return retval;
        }

        private static List<SyntaxToken> GetCSMemberIdentifier(this IEnumerable<VBS.MemberDeclarationSyntax> csMembers)
        {
            List<SyntaxToken> ids = new List<SyntaxToken>();
            foreach (VBS.MemberDeclarationSyntax csMember in csMembers)
            {
                switch (csMember.Kind())
                {
                case VB.SyntaxKind.DelegateDeclaration:
                    break;
                case VB.SyntaxKind.EventDeclaration:
                    ids.Add(((VBS.EventDeclarationSyntax)csMember).Identifier);
                    break;
                case VB.SyntaxKind.EventFieldDeclaration:
                case VB.SyntaxKind.FieldDeclaration:
                    foreach (VBS.VariableDeclaratorSyntax csv in ((VBS.BaseFieldDeclarationSyntax)csMember).Declaration.Variables)
                    {
                        ids.Add(csv.Identifier);
                    }
                    break;
                case VB.SyntaxKind.MethodDeclaration:
                    ids.Add(((VBS.MethodDeclarationSyntax)csMember).Identifier);
                    break;
                case VB.SyntaxKind.PropertyDeclaration:
                    ids.Add(((VBS.PropertyDeclarationSyntax)csMember).Identifier);
                    break;
                case VB.SyntaxKind.EnumMemberDeclaration:
                    ids.Add(((VBS.EnumMemberDeclarationSyntax)csMember).Identifier);
                    break;
                case VB.SyntaxKind.EnumDeclaration:
                case VB.SyntaxKind.ClassDeclaration:
                case VB.SyntaxKind.StructDeclaration:
                case VB.SyntaxKind.InterfaceDeclaration:
                    ids.Add(((VBS.BaseTypeDeclarationSyntax)csMember).Identifier);
                    break;

                case VB.SyntaxKind.OperatorDeclaration:
                case VB.SyntaxKind.ConversionOperatorDeclaration:
                case VB.SyntaxKind.ConstructorDeclaration:
                case VB.SyntaxKind.DestructorDeclaration:
                case VB.SyntaxKind.IndexerDeclaration:
                    break;
                case VB.SyntaxKind.IncompleteMember:
                    //Because input is incomplete.
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "GetCSMemberIdentifier");
                    break;
                }
            }
            return ids;
        }

        #endregion
    }


}

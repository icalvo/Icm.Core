
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections.Generic;

[Category("Icm")]
[TestFixture()]
public class DictionaryExtensionsTest
{

	public class DictionaryExtensionsTestCases
	{

		static readonly IDictionary<string, string> target = new Dictionary<string, string> {
			{
				"doc",
				"Doc word"
			},
			{
				"xls",
				"Excel"
			},
			{
				"pdf",
				"Adobe PDF"
			},
			{
				"",
				"No extension"
			}

		};

		static Converter<string, string> converter = str => "-- " + str;
		public static IEnumerable<TestCaseData> GetItemOrDefaultCases {
			get {
				List<TestCaseData> result = new List<TestCaseData>();
				result.Add(new TestCaseData(target, "doc", "DEFAULT").Returns("Doc word"));
				result.Add(new TestCaseData(target, "", "DEFAULT").Returns("No extension").SetName("EmptyKey"));
				result.Add(new TestCaseData(target, "ppt", "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"));
				result.Add(new TestCaseData(target, "ppt", null).Returns(null).SetName("KeyNotFoundDefaultNull"));
				result.Add(new TestCaseData(target, null, "DEFAULT").Throws(typeof(ArgumentNullException)));

				return result;
			}
		}

		public static IEnumerable<TestCaseData> GetItemOrDefaultCases_WithConverter {
			get {
				List<TestCaseData> result = new List<TestCaseData>();
				result.Add(new TestCaseData(target, "doc", converter, "DEFAULT").Returns("-- Doc word"));
				result.Add(new TestCaseData(target, "", converter, "DEFAULT").Returns("-- No extension").SetName("EmptyKey"));
				result.Add(new TestCaseData(target, "ppt", converter, "DEFAULT").Returns("DEFAULT").SetName("KeyNotFound"));
				result.Add(new TestCaseData(target, "ppt", converter, null).Returns(null).SetName("KeyNotFoundDefaultNull"));
				result.Add(new TestCaseData(target, null, converter, "DEFAULT").Throws(typeof(ArgumentNullException)));

				return result;
			}
		}

		static object[] GetMergeCases = {
			new object[] {
				new Dictionary<string, string>(),
				new Dictionary<string, string>(),
				new Dictionary<string, string>()
			},
			new object[] {
				new Dictionary<string, string> { {
					"pdf",
					"Adobe PDF"
				} },
				new Dictionary<string, string> {
					{
						"doc",
						"Microsoft Word"
					},
					{
						"xls",
						"Microsoft Excel"
					}
				},
				new Dictionary<string, string> {
					{
						"doc",
						"Microsoft Word"
					},
					{
						"pdf",
						"Adobe PDF"
					},
					{
						"xls",
						"Microsoft Excel"
					}
				}
			},
			new object[] {
				new Dictionary<string, string> {
					{
						"pdf",
						"Adobe PDF"
					},
					{
						"doc",
						"MSWORD"
					}
				},
				new Dictionary<string, string> {
					{
						"doc",
						"Microsoft Word"
					},
					{
						"xls",
						"Microsoft Excel"
					}
				},
				new Dictionary<string, string> {
					{
						"doc",
						"Microsoft Word"
					},
					{
						"pdf",
						"Adobe PDF"
					},
					{
						"xls",
						"Microsoft Excel"
					}
				}
			}

		};
	}

	[TestCaseSource(typeof(DictionaryExtensionsTestCases), "GetItemOrDefaultCases")]
	public string ItemOrDefault(IDictionary<string, string> target, string key, string defaultResult)
	{
		return target.ItemOrDefault(key, defaultResult);
	}

	[TestCaseSource(typeof(DictionaryExtensionsTestCases), "GetItemOrDefaultCases_WithConverter")]
	public string ItemOrDefault_WithConverter(IDictionary<string, string> target, string key, Converter<string, string> converter, string defaultResult)
	{
		return target.ItemOrDefault(key, converter, defaultResult);
	}

	[TestCaseSource(typeof(DictionaryExtensionsTestCases), "GetMergeCases")]
	public void Merge(IDictionary<string, string> target, IDictionary<string, string> other, IDictionary<string, string> expectedTarget)
	{
		Dictionary<string, string> cloneOther = new Dictionary<string, string>(other);

		target.Merge(other);

		Assert.That(other, Is.EquivalentTo(cloneOther));
		Assert.That(target, Is.EquivalentTo(expectedTarget));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

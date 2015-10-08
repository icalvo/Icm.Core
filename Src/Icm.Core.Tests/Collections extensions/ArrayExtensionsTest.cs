
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Icm.Collections;

[TestFixture(), Category("Icm")]
public class ArrayExtensionsTest
{

	static readonly object[] MultiGetRow_NormalStringTestCases = { new object[] {
		new string[ + 1,  + 1] {
			{
				"maria",
				"rafa"
			},
			{
				"juan",
				"pepe"
			}
		},
		0,
		new int[] { 1 },
		new string[] {
			"rafa",
			"pepe"
		}

	} };
	static readonly object[] MultiGetRow_NormalIntegerTestCases = {
		new object[] {
			new int[ + 1,  + 1] {
				{
					1,
					5
				},
				{
					2,
					7
				},
				{
					3,
					9
				}
			},
			1,
			new int[] { 1 },
			new int[] {
				2,
				7
			}
		},
		new object[] {
			new int[ + 1,  + 1,  + 1] {
				{
					{
						11,
						15,
						14
					},
					{
						12,
						17,
						16
					},
					{
						13,
						19,
						18
					}
				},
				{
					{
						21,
						25,
						24
					},
					{
						22,
						27,
						26
					},
					{
						23,
						29,
						28
					}
				},
				{
					{
						31,
						35,
						34
					},
					{
						32,
						37,
						36
					},
					{
						33,
						39,
						38
					}
				}
			},
			0,
			new int[] {
				1,
				1
			},
			new int[] {
				17,
				27,
				37
			}
		},
		new object[] {
			new int[ + 1,  + 1,  + 1] {
				{
					{
						11,
						15,
						14
					},
					{
						12,
						17,
						16
					},
					{
						13,
						19,
						18
					}
				},
				{
					{
						21,
						25,
						24
					},
					{
						22,
						27,
						26
					},
					{
						23,
						29,
						28
					}
				},
				{
					{
						31,
						35,
						34
					},
					{
						32,
						37,
						36
					},
					{
						33,
						39,
						38
					}
				}
			},
			1,
			new int[] {
				1,
				2
			},
			new int[] {
				24,
				26,
				28
			}
		}

	};

	static readonly object[] MultiGetRow_ExceptionalStringTestCases = {
		new object[] {
			new string[ + 1,  + 1] {
				{
					"",
					""
				},
				{
					"",
					""
				}
			},
			0,
			new int[] { 2 },
			typeof(IndexOutOfRangeException)
		},
		new object[] {
			new string[ + 1,  + 1] {
				{
					"",
					""
				},
				{
					"",
					""
				}
			},
			-1,
			new int[] { 1 },
			typeof(IndexOutOfRangeException)
		},
		new object[] {
			new string[ + 1,  + 1] {
				{
					"",
					""
				},
				{
					"",
					""
				}
			},
			0,
			new int[] { -1 },
			typeof(IndexOutOfRangeException)
		},
		new object[] {
			new string[ + 1,  + 1] {
				{
					"",
					""
				},
				{
					"",
					""
				}
			},
			5,
			new int[] { 1 },
			typeof(IndexOutOfRangeException)
		},
		new object[] {
			new string[ + 1,  + 1,  + 1] {
				{
					{
						"",
						""
					},
					{
						"",
						""
					}
				},
				{
					{
						"",
						""
					},
					{
						"",
						""
					}
				}
			},
			0,
			new int[] { 1 },
			typeof(ArgumentException)
		}

	};

	[Test()]
	[TestCaseSource("MultiGetRow_NormalStringTestCases")]
	public void MultiGetRow_ReturnsExpected(string[,] target, int iteratingDimension, int[] fixedDimensionValues, string[] expected)
	{
		dynamic actual = target.MultiGetRow<string>(iteratingDimension, fixedDimensionValues);
		Assert.That(actual, Is.EquivalentTo(expected));
	}

	[Test()]
	[TestCaseSource("MultiGetRow_NormalIntegerTestCases")]
	public void MultiGetRow_ReturnsExpected(Array target, int iteratingDimension, int[] fixedDimensionValues, int[] expected)
	{
		dynamic actual = target.MultiGetRow<int>(iteratingDimension, fixedDimensionValues);
		Assert.That(actual, Is.EquivalentTo(expected));
	}

	[Test()]
	[TestCaseSource("MultiGetRow_ExceptionalStringTestCases")]
	public void MultiGetRow_ThrowsIndexOutOfRange(Array target, int iteratingDimension, int[] fixedDimensionValues, Type exceptionType)
	{
		Assert.That(() => target.MultiGetRow<string>(iteratingDimension, fixedDimensionValues), Throws.TypeOf(exceptionType));
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

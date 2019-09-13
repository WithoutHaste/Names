using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Names.Domain;
using Names.Domain.Entities;
using Names.Read.UseCases;
using Names.Read.UseCases.Data;

namespace Names.Read.UseCases.Test
{
	[TestClass]
	public class TestGetCategoryTree
	{
		[TestMethod]
		public void Execute_TopLevelBecauseNull()
		{
			//assign
			List<CategoryRecord> records = new List<CategoryRecord>() {
				new CategoryRecord() {
					Category = "A",
					SuperCategory = null
				}
			};

			Mock<INamesGateway> mockGateway = new Mock<INamesGateway>();
			mockGateway.Setup(mock => mock.GetCategories()).Returns(records);

			//act
			List<CategoryOutput> result = GetCategoryTree.Execute(mockGateway.Object).ToList();

			//assert
			Assert.AreEqual(1, result.Count, "Result set count.");
			Assert.AreEqual(records[0].Category, result[0].Category, "Category label.");
		}

		[TestMethod]
		public void Execute_TopLevelBecauseEmptyString()
		{
			//assign
			List<CategoryRecord> records = new List<CategoryRecord>() {
				new CategoryRecord() {
					Category = "A",
					SuperCategory = ""
				}
			};

			Mock<INamesGateway> mockGateway = new Mock<INamesGateway>();
			mockGateway.Setup(mock => mock.GetCategories()).Returns(records);

			//act
			List<CategoryOutput> result = GetCategoryTree.Execute(mockGateway.Object).ToList();

			//assert
			Assert.AreEqual(1, result.Count, "Result set count.");
			Assert.AreEqual(records[0].Category, result[0].Category, "Category label.");
		}

		[TestMethod]
		public void Execute_MultipleTopLevelCategory_MultipleChildrenEach()
		{
			//assign
			List<CategoryRecord> records = new List<CategoryRecord>() {
				new CategoryRecord() {
					Category = "TopA",
					SuperCategory = null
				},
				new CategoryRecord() {
					Category = "TopB",
					SuperCategory = null
				},
				new CategoryRecord() {
					Category = "ChildA1",
					SuperCategory = "TopA"
				},
				new CategoryRecord() {
					Category = "ChildA2",
					SuperCategory = "TopA"
				},
				new CategoryRecord() {
					Category = "ChildB1",
					SuperCategory = "TopB"
				},
				new CategoryRecord() {
					Category = "ChildB2",
					SuperCategory = "TopB"
				},
			};

			Mock<INamesGateway> mockGateway = new Mock<INamesGateway>();
			mockGateway.Setup(mock => mock.GetCategories()).Returns(records);

			//act
			List<CategoryOutput> result = GetCategoryTree.Execute(mockGateway.Object).ToList();

			//assert
			result = result.OrderBy(x => x.Category).ToList();
			result[0].SubCategories = result[0].SubCategories.OrderBy(x => x.Category).ToList();
			result[1].SubCategories = result[1].SubCategories.OrderBy(x => x.Category).ToList();
			Assert.AreEqual(2, result.Count, "Result set count.");
			Assert.AreEqual("TopA", result[0].Category, "Category label.");
			Assert.AreEqual(2, result[0].SubCategories.Count, "Child count 1.");
			Assert.AreEqual("ChildA1", result[0].SubCategories[0].Category, "Category label.");
			Assert.AreEqual("ChildA2", result[0].SubCategories[1].Category, "Category label.");
			Assert.AreEqual("TopB", result[1].Category, "Category label.");
			Assert.AreEqual(2, result[1].SubCategories.Count, "Child count 2.");
			Assert.AreEqual("ChildB1", result[1].SubCategories[0].Category, "Category label.");
			Assert.AreEqual("ChildB2", result[1].SubCategories[1].Category, "Category label.");
		}

		[TestMethod]
		public void Execute_CategoryWithMultipleSubCategoriesAllowed()
		{
			//assign
			List<CategoryRecord> records = new List<CategoryRecord>() {
				new CategoryRecord() {
					Category = "A",
					SuperCategory = null
				},
				new CategoryRecord() {
					Category = "B",
					SuperCategory = "A"
				},
				new CategoryRecord() {
					Category = "C",
					SuperCategory = "A"
				},
				new CategoryRecord() {
					Category = "D",
					SuperCategory = "B"
				},
				new CategoryRecord() {
					Category = "D",
					SuperCategory = "C"
				},
			};

			Mock<INamesGateway> mockGateway = new Mock<INamesGateway>();
			mockGateway.Setup(mock => mock.GetCategories()).Returns(records);

			//act
			List<CategoryOutput> result = GetCategoryTree.Execute(mockGateway.Object).ToList();

			//assert
			result = result.OrderBy(x => x.Category).ToList();
			result[0].SubCategories = result[0].SubCategories.OrderBy(x => x.Category).ToList();
			Assert.AreEqual(1, result.Count, "Result set count.");
			Assert.AreEqual("A", result[0].Category, "Category label.");
			Assert.AreEqual(2, result[0].SubCategories.Count, "Child count 1.");
			Assert.AreEqual("B", result[0].SubCategories[0].Category, "Category label.");
			Assert.AreEqual("C", result[0].SubCategories[1].Category, "Category label.");
			Assert.AreEqual(1, result[0].SubCategories[0].SubCategories.Count, "Child count 2.");
			Assert.AreEqual("D", result[0].SubCategories[0].SubCategories[0].Category, "Category label.");
			Assert.AreEqual(1, result[0].SubCategories[1].SubCategories.Count, "Child count 3.");
			Assert.AreEqual("D", result[0].SubCategories[1].SubCategories[0].Category, "Category label.");
		}

		[TestMethod]
		public void Execute_LoopsDontCauseErrors()
		{
			//assign
			List<CategoryRecord> records = new List<CategoryRecord>() {
				new CategoryRecord() {
					Category = "A",
					SuperCategory = null
				},
				new CategoryRecord() {
					Category = "B",
					SuperCategory = "A"
				},
				new CategoryRecord() {
					Category = "C",
					SuperCategory = "B"
				},
				new CategoryRecord() {
					Category = "D",
					SuperCategory = "C"
				},
				new CategoryRecord() {
					Category = "B",
					SuperCategory = "D"
				},
			};

			Mock<INamesGateway> mockGateway = new Mock<INamesGateway>();
			mockGateway.Setup(mock => mock.GetCategories()).Returns(records);

			//act
			List<CategoryOutput> result = GetCategoryTree.Execute(mockGateway.Object).ToList();

			//assert
			result = result.OrderBy(x => x.Category).ToList();
			Assert.AreEqual(1, result.Count, "Result set count.");
			Assert.AreEqual("A", result[0].Category, "Category label.");
			Assert.AreEqual(1, result[0].SubCategories.Count, "Child count 1.");
			Assert.AreEqual("B", result[0].SubCategories[0].Category, "Category label.");
			Assert.AreEqual(1, result[0].SubCategories[0].SubCategories.Count, "Child count 2.");
			Assert.AreEqual("C", result[0].SubCategories[0].SubCategories[0].Category, "Category label.");
			Assert.AreEqual(1, result[0].SubCategories[0].SubCategories[0].SubCategories.Count, "Child count 3.");
			Assert.AreEqual("D", result[0].SubCategories[0].SubCategories[0].SubCategories[0].Category, "Category label.");
		}
	}
}

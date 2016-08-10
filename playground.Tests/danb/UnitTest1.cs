using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Playground.Areas.Danb.Models;

namespace Playground.Danb.ModelTest
{
	[TestFixture]
	class DanbModelTests
	{
		[Test]
		public void TestMethod1()
		{
			var a = new DanBModel();
			int value = a.GetNumber();
			Assert.True( value == 3 );

		}
		[Test]
		public void TestMethod2()
		{
			Assert.True( true );
		}
	}
}

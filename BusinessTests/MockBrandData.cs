using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Xunit;

namespace BusinessTests
{
    public class MockBrandData : TheoryData<Brand>
    {
        public MockBrandData()
        {
            Add(new Brand()
            {
                BrandId = 1,
                BrandName = "Mercedes"
            });
            Add(new Brand()
            {
                BrandId = 2,
                BrandName = "Audi"
            });
            Add(new Brand()
            {
                BrandId = 3,
                BrandName = "BMW"
            });
        }
    }
}

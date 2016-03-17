using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Moq;
using Xunit;

namespace Alive.Net.UnitTests
{
    public class MainUnitTests
    {
        [Fact]
        public void ShouldThrowIfNoNext()
        {
            Assert.Throws<ArgumentNullException>(() => new Alive(null, new AliveOptions()));
        }

        [Fact]
        public void ShouldConstructIfOptionsIsNull()
        {
            var mockedContext = new Mock<HttpContext>();
            var al = new Alive((context) => Task.Delay(0), null);
        }

        [Fact]
        public void CalculateResponseThrowsOnNullOptions()
        {
            Assert.Throws<ArgumentNullException>(() => Alive.CalculateResponse(null));
        }

        [Fact]
        public void EnsureLivecheckFuncOverridesReturnData()
        {
            var t = new AliveOptions
            {
                BodyText = "awesome",
                StatusCode = System.Net.HttpStatusCode.MovedPermanently,
                OnLivecheckResponse = (d) => { d.StatusCode = System.Net.HttpStatusCode.Moved; }
            };
            var calculatedResponse = Alive.CalculateResponse(t);
            Assert.True(calculatedResponse.StatusCode == System.Net.HttpStatusCode.Moved);
        }
    }
}
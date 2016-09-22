using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace GetMime.UnitTest
{
    [TestClass]
    public class MimeTests
    {
        private const string ApplicationOctetStream = "application/octet-stream";

        [TestMethod]
        public void EmptyTxt()
        {
            Mime.Get("../../App_Data/empty.txt").ShouldNotBe(ApplicationOctetStream);
            Mime.Get("../../App_Data/empty.txt").ShouldBe("application/text");
        }

        [TestMethod]
        public void Excel_New()
        {
            Mime.Get("../../App_Data/test_excel.xlsx").ShouldNotBe(ApplicationOctetStream);
            Mime.Get("../../App_Data/test_excel.xlsx").ShouldBe("application/text");
        }

        [TestMethod]
        public void Excel_RealWorld()
        {
            Mime.Get("../../App_Data/example_excel.xlsx").ShouldNotBe(ApplicationOctetStream);
            Mime.Get("../../App_Data/example_excel.xlsx").ShouldBe("application/text");
        }

        [TestMethod]
        public void Pdf()
        {
            Mime.Get("../../App_Data/example_pdf.pdf").ShouldNotBe(ApplicationOctetStream);
            Mime.Get("../../App_Data/example_pdf.pdf").ShouldBe("application/text");
        }

        [TestMethod]
        public void Text()
        {
            Mime.Get("../../App_Data/Text.txt").ShouldNotBe(ApplicationOctetStream);
            Mime.Get("../../App_Data/Text.txt").ShouldBe("application/text");
        }
    }
}
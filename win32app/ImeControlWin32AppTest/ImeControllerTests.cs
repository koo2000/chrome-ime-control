
namespace Tests
{

    using ImeControl;

    public class ImeControllerTests
    {


        [Test]
        public void testImeOffEdge()
        {
            ImeController controller = new ImeController("msedge");

            controller.setImeActiveStatus(false);

        }

        [Test]
        public void testImeOnEdge()
        {
            ImeController controller = new ImeController("msedge");

            controller.setImeActiveStatus(true);

        }

        [Test]
        public void testImeOffChrome()
        {
            ImeController controller = new ImeController("chrome");

            controller.setImeActiveStatus(false);

        }

        [Test]
        public void testImeOnChrome()
        {
            ImeController controller = new ImeController("chrome");

            controller.setImeActiveStatus(true);

        }
    }
}

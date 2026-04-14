
namespace Tests
{

    using ImeControl;

    public class ImeControllerTests
    {


        [Test]
        public void testImeOffEdge()
        {
            ImeController controller = new ImeController();

            controller.setImeActiveStatus("msedge", false);

        }

        [Test]
        public void testImeOnEdge()
        {
            ImeController controller = new ImeController();

            controller.setImeActiveStatus("msedge", true);

        }

        [Test]
        public void testImeOffChrome()
        {
            ImeController controller = new ImeController();

            controller.setImeActiveStatus("chrome", false);

        }

        [Test]
        public void testImeOnChrome()
        {
            ImeController controller = new ImeController();

            controller.setImeActiveStatus("chrome", true);

        }
    }
}

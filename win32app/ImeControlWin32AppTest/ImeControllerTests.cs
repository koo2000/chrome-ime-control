
namespace Tests
{

    using ImeControl;

    public class ImeControllerTests
    {


        [Test]
        public void testImeOff()
        {
            ImeController controller = new ImeController("msedge");

            controller.setImeActiveStatus(false);

        }

        [Test]
        public void testImeOn()
        {
            ImeController controller = new ImeController("msedge");

            controller.setImeActiveStatus(true);

        }

    }
}


namespace Tests
{


    using ImeControl;


    public class Tests
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


namespace Tests
{
    using System.Data;
    using ImeControl;

    public class ImeControllerTests
    {


        [Test]
        public void testImeOffEdge()
        {
            ImeController controller = new ImeController();

            controller.setImeStatus("msedge", false);

        }

        [Test]
        public void testImeOnEdge()
        {
            ImeController controller = new ImeController();

            controller.setImeStatus("msedge", true);

        }

        [Test]
        public void testImeFullHiraganaOnEdge()
        {
            ImeController controller = new ImeController();

            int mode = ImeController.IME_CMODE_JAPANESE |  ImeController.IME_CMODE_FULLSHAPE;
            controller.setImeStatus("msedge", true, mode);
        }

        [Test]
        public void testImeFullKatakanaOnEdge()
        {
            ImeController controller = new ImeController();

            int mode = ImeController.IME_CMODE_JAPANESE | ImeController.IME_CMODE_KATAKANA| ImeController.IME_CMODE_FULLSHAPE;
            controller.setImeStatus("msedge", true, mode);
        }

        [Test]
        public void testImeHankakuKanaOnEdge()
        {
            ImeController controller = new ImeController();

            int mode = ImeController.IME_CMODE_JAPANESE | ImeController.IME_CMODE_KATAKANA;
            controller.setImeStatus("msedge", true, mode);
        }

        [Test]
        public void testImeOffChrome()
        {
            ImeController controller = new ImeController();

            controller.setImeStatus("chrome", false);

        }

        [Test]
        public void testImeOnChrome()
        {
            ImeController controller = new ImeController();

            controller.setImeStatus("chrome", true);

        }

        
        [Test]
        public void testImeHankakuKanaOnChrome()
        {
            ImeController controller = new ImeController();

            int mode = ImeController.IME_CMODE_JAPANESE | ImeController.IME_CMODE_KATAKANA;
            controller.setImeStatus("chrome", true, mode);
        }

    }
}

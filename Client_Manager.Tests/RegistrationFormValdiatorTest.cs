using Client_Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls;

namespace Client_Manager.Tests
{
    [TestClass]
    public class RegistrationFormvalidatorTest
    {

        private RegistrationFormValidator validator;

        [TestMethod]
        public void ValidateUser_WhenEmailCorrect_ReturnsTrue()
        {
            var manager = new MockManager();
            manager.Result = true;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox()};
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "a.a@a.a";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsTrue(result);
        }
        public void ValidateClient_WhenEmailIncorrect_ReturnsFalse()
        {
            var manager = new MockManager();
            manager.Result = false;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "a.a@a.a999999999XDDDD’’’’’@hehe";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsFalse(result);

        }
        public void ValidateClient_WhenPhoneNumberCorrectWhile_ReturnsTrue()
        {
            var manager = new MockManager();
            manager.Result = true;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "a.a@a.a";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsTrue(result);
        }
        public void ValidateClient_WhenPhoneNumberIncorrect_ReturnsFalse()
        {
            var manager = new MockManager();
            manager.Result = false;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "666";
            boxes[2].Text = "a.a@a.a";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsFalse(result);
        }
        public void ValidateClient_WhenNameCorrect_ReturnsTrue()
        {
            var manager = new MockManager();
            manager.Result = true;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "a.a@a.a";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsTrue(result);
        }
        public void ValidateClient_WhenNameInCorrect_ReturnsFalse()
        {
            var manager = new MockManager();
            manager.Result = false;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "1337 Leet Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "a.a@a.a";
            var result = validator.ValidateClientInput(boxes.ToList());

            Assert.IsFalse(result);
        }
        public void ValidateCar_WhenBrandCorrect_ReturnsTrue()
        {
            var manager = new MockManager();
            manager.Result = true;
            validator = new RegistrationFormValidator(false, manager);
            TextBox[] boxes = new TextBox[3] { new TextBox(), new TextBox(), new TextBox() };
            boxes[0].Text = "Teszt Elek";
            boxes[1].Text = "1234567891011";
            boxes[2].Text = "TEST-1337";
            var result = validator.ValidateAutoInput(boxes.ToList());

            Assert.IsTrue(result);
        }
        
        

    }
}

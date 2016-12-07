using System;

namespace Assets.Scripts
{
    public class Hint
    {
        private int price;

        private string text;

        public Hint(string text, int price)
        {
            this.Text = text;
            this.Price = price;
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The hint text can't!");
                }

                this.text = value;
            }
        }

        public int Price
        {
            get { return this.price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The hint price can't be negative!");
                }

                this.price = value;
            }
        }
    }
}
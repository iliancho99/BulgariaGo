using System;

namespace Assets.Scripts
{
    public class Question
    {
        private string[] answers;
        private int correctAnswerId;
        private string questionText;

        public Question(string questionText, string[] answers, int correctAnswerId)
        {
            this.QuestionText = questionText;
            this.Answers = answers;
            this.CorrectAnswerId = correctAnswerId;
        }

        public string QuestionText
        {
            get { return questionText; }
            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The Question Text can't be null!");
                }

                questionText = value;
            }
        }

        public string[] Answers
        {
            get { return answers; }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException("The answers can't be empty!");
                }

                this.answers = value;
            }
        }

        public int CorrectAnswerId
        {
            get { return this.correctAnswerId; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The Correct Answer id can't be negative!");
                }

                this.correctAnswerId = value;
            }
        }
    }
}
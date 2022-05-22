using QuickPoll.Shared;
using System.Text;

namespace QuickPoll.Client.Analyzer
{
    public class NewPollAnalyzer
    {
        List<string> ListAnalyzeResults { get; set; }

        private Poll AnalyzedPoll { get; set; }

        public string? Analyze(Poll poll)
        {
            try
            {
                if (poll is null)
                    return String.Empty;

                AnalyzedPoll = poll;

                ListAnalyzeResults = new();

                CheckForDuplicateAnswers();
                AreAllCapsUsed();
                CheckIsPollLongToRead();

                return FormatResults(ListAnalyzeResults);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void CheckForDuplicateAnswers()
        {
            Func<List<string>, bool> checkDuplicate = (List<string> listStr) =>
                {
                    if (listStr is null || listStr.Count is 0 or 1)
                        return false;

                    listStr.Sort();

                    for (int i = 0; i < listStr.Count; i++)
                    {
                        if (string.IsNullOrWhiteSpace(listStr[i]))
                            continue;

                        if (listStr[i] == listStr[i + 1])
                            return true;
                    }

                    return false;
                };

            List<string> allAnswersText = AnalyzedPoll.PossibleAnswers.Select(x => x.AnswerText).ToList();

            if (checkDuplicate(allAnswersText))
                ListAnalyzeResults.Add("Duplicate answers found.");
        }

        private void AreAllCapsUsed()
        {
            bool allCapsUsed = false;

            if (ContainsLetter(AnalyzedPoll.Question) && AnalyzedPoll.Question.ToUpper() == AnalyzedPoll.Question)
            {
                allCapsUsed = true;
            }
            else
            {
                for (int i = 0; i < AnalyzedPoll.PossibleAnswers.Count; i++)
                {
                    string answer = AnalyzedPoll.PossibleAnswers[i].AnswerText;
                    if (ContainsLetter(answer) && answer.ToUpper() == answer)
                    {
                        allCapsUsed = true;
                        break;
                    }
                }
            }

            if (allCapsUsed)
                ListAnalyzeResults.Add("Suggestion: Do not use all caps in an answer.");
        }

        private void CheckIsPollLongToRead()
        {
            CheckIsQuestionLongToRead();
            CheckAreAnswersLongToRead();
        }

        private void CheckIsQuestionLongToRead()
        {
            if (AnalyzedPoll?.Question is null)
                return;

            int maxRecommendedLength = MaxRecommendedLengthForQuestion();

            if (AnalyzedPoll.Question.Length > maxRecommendedLength)
                ListAnalyzeResults.Add("Question is long. A shorter question would be easier to read.");
        }

        private void CheckAreAnswersLongToRead()
        {
            int countAnswerToLong = 0;

            for (int i = 0; i < AnalyzedPoll.PossibleAnswers.Count; i++)
            {
                if (AnalyzedPoll.PossibleAnswers[i].AnswerText is null)
                    continue;

                int maxRecommendedLength = MaxRecommendedLengthForAnswer();

                int lengthCurrentAnswer = AnalyzedPoll.PossibleAnswers[i].AnswerText.Length;

                if (lengthCurrentAnswer > maxRecommendedLength)
                    countAnswerToLong++;
            }

            if (countAnswerToLong is 1)
                ListAnalyzeResults.Add($"One answer is long. Shorter answer would be easier to read.");
            else if (countAnswerToLong > 1)
                ListAnalyzeResults.Add($"{countAnswerToLong} answers are long. Shorter answers would be easier to read.");
        }

        private string? FormatResults(List<string> listAnalyzeResults)
        {
            if (listAnalyzeResults is null || listAnalyzeResults.Count == 0)
                return "Poll looks great.";

            StringBuilder sbFormattedResults = new();

            foreach (string resultLine in ListAnalyzeResults)
            {
                sbFormattedResults.AppendLine(resultLine);
                sbFormattedResults.AppendLine("<br/>");
            }

            return sbFormattedResults.ToString();
        }

        public bool ContainsLetter(string? str) => str?.Any(x => char.IsLetter(x)) ?? false;

        private int MaxRecommendedLengthForAnswer()
        {
            return 200; // characters
        }

        private int MaxRecommendedLengthForQuestion()
        {
            return 250; // characters
        }
    }
}

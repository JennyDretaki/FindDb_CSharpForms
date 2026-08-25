using System.Text;

namespace FindDb.Services
{
    public static class FuzzySearch
    {
        public static int Similarity(
            string source,
            string target)
        {
            if (string.IsNullOrWhiteSpace(source) ||
                string.IsNullOrWhiteSpace(target))
            {
                return 0;
            }

            string normalizedSource = Normalize(source);
            string normalizedTarget = Normalize(target);

            if (normalizedSource == normalizedTarget)
                return 100;

            if (normalizedTarget.Contains(normalizedSource))
                return 98;

            if (normalizedSource.Contains(normalizedTarget))
                return 95;

            // ---------------------------------------
            // Multiple keywords
            // ---------------------------------------

            string[] keywords = GetKeywords(source);

            if (keywords.Length > 1)
            {
                int matchedKeywords = keywords.Count(
                    x => normalizedTarget.Contains(Normalize(x)));

                if (matchedKeywords > 0)
                {
                    int keywordScore =
                        (int)((double)matchedKeywords /
                              keywords.Length * 95);

                    if (matchedKeywords == keywords.Length)
                        return 97;

                    if (keywordScore >= 50)
                        return keywordScore;
                }
            }

            // ---------------------------------------
            // Levenshtein
            // ---------------------------------------

            int distance = LevenshteinDistance(
                normalizedSource,
                normalizedTarget);

            int maxLength = Math.Max(
                normalizedSource.Length,
                normalizedTarget.Length);

            if (maxLength == 0)
                return 100;

            double similarity =
                1.0 - (double)distance / maxLength;

            int score =
                (int)Math.Round(similarity * 100);

            return Math.Max(0, score);
        }

        public static bool ContainsAllKeywords(
            string value,
            string searchText)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            string normalizedValue = Normalize(value);

            string[] keywords =
                GetKeywords(searchText);

            return keywords.All(
                keyword => normalizedValue.Contains(
                    Normalize(keyword)));
        }

        private static string[] GetKeywords(string value)
        {
            return value.Split(
                new[]
                {
                    ' ',
                    '_',
                    '-',
                    '.',
                    '/',
                    '\\'
                },
                StringSplitOptions.RemoveEmptyEntries);
        }

        public static string Normalize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            StringBuilder builder = new();

            foreach (char c in value.ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(c))
                    builder.Append(c);
            }

            return builder.ToString();
        }

        private static int LevenshteinDistance(
            string source,
            string target)
        {
            int[,] distance =
                new int[
                    source.Length + 1,
                    target.Length + 1];

            for (int i = 0; i <= source.Length; i++)
                distance[i, 0] = i;

            for (int j = 0; j <= target.Length; j++)
                distance[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost =
                        source[i - 1] == target[j - 1]
                            ? 0
                            : 1;

                    distance[i, j] =
                        Math.Min(
                            Math.Min(
                                distance[i - 1, j] + 1,
                                distance[i, j - 1] + 1),
                            distance[i - 1, j - 1] + cost);
                }
            }

            return distance[
                source.Length,
                target.Length];
        }
    }
}
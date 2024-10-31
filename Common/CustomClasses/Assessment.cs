namespace Common.CustomClasses;
public static class Assessment
{
    public static double GWAPercentage(double GWA)
    {
        // Calculate the gwa
        double result = GWA * 0.20;

        // Return the result
        return Math.Round(result, 2);
    }

    public static string ReadingInterpretation(int ReadingRawScore)
    {
        string result = ReadingRawScore >= 0 && ReadingRawScore <= 31 ? "BA" : ReadingRawScore >= 32 && ReadingRawScore <= 49 ? "A" : "AA";
        return result;

    }
    public static double ReadingEquivalent(int ReadingRawScore)
    {
        // Calculate the reading equivalent score
        double result = (ReadingRawScore / 55.0) * 35.0 + 60.0;
        // Return the result
        return Math.Round(result, 2);
    }
    public static string MathInterpretation(int MathRawScore)
    {
        string result = MathRawScore >= 0 && MathRawScore <= 26 ? "BA" : MathRawScore >= 27 && MathRawScore <= 39 ? "A" : "AA";
        return result;

    }
    public static double MathEquivalent(int MathRawScore)
    {
        // Calculate the math equivalent score
        double result = (MathRawScore / 50.0) * 35.0 + 60.0;

        // Return the result
        return Math.Round(result, 2);
    }

    public static string ScienceInterpretation(int ScienceRawScore)
    {
        string result = ScienceRawScore >= 0 && ScienceRawScore <= 31 ? "BA" : ScienceRawScore >= 32 && ScienceRawScore <= 49 ? "A" : "AA";
        return result;
    }
    public static double ScienceEquivalent(int ScienceRawScore)
    {
        // Calculate the science equivalent score
        double result = (ScienceRawScore / 55.0) * 35.0 + 60.0;

        // Return the result
        return Math.Round(result, 2);
    }

    public static double TotalAchievementEquivalent(int readingRawScore, int mathRawScore, int scienceRawScore)
    {
        // Calculate the total equivalent score
        double result = (ReadingEquivalent(readingRawScore) + MathEquivalent(mathRawScore) + ScienceEquivalent(scienceRawScore)) / 3.0;
        // Return the result
        return Math.Round(result, 2);
    }
    public static string IntelligenceInterpretation(int IntelligenceRawScore)
    {
        string result =
                IntelligenceRawScore >= 0 && IntelligenceRawScore <= 41 ? "Very Low"
                : IntelligenceRawScore >= 42 && IntelligenceRawScore <= 47 ? "Low"
                : IntelligenceRawScore >= 48 && IntelligenceRawScore <= 55 ? "BA"
                : IntelligenceRawScore >= 56 && IntelligenceRawScore <= 64 ? "LA"
                : IntelligenceRawScore >= 65 && IntelligenceRawScore <= 74 ? "A"
                : IntelligenceRawScore >= 75 && IntelligenceRawScore <= 82 ? "HA"
                : IntelligenceRawScore >= 83 && IntelligenceRawScore <= 89 ? "AA"
                : IntelligenceRawScore >= 90 && IntelligenceRawScore <= 95 ? "H"
                : "VH";
        return result;

    }
    public static double IntelligenceEquivalent(int IntelligenceRawScore)
    {
        // Calculate the Intelligence equivalent score
        double result = (IntelligenceRawScore / 135.0) * 35.0 + 60.0;

        // Return the result
        return Math.Round(result, 2);
    }
    public static double TotalAchievementIntelligenceEquivalent(int readingRawScore, int mathRawScore, int scienceRawScore, int intelligenceRawScore)
    {
        // Calculate the total equivalent score
        double result = (TotalAchievementEquivalent(readingRawScore, mathRawScore, scienceRawScore) + IntelligenceEquivalent(intelligenceRawScore)) / 2.0;

        // Return the result
        return Math.Round(result, 2); ;
    }

    public static double ExaminationResult(int readingRawScore, int mathRawScore, int scienceRawScore, int intelligenceRawScore)
    {
        // Calculate the total equivalent score
        double result = (TotalAchievementIntelligenceEquivalent(readingRawScore, mathRawScore, scienceRawScore, intelligenceRawScore)) * 0.5;
        // Return the result
        return Math.Round(result, 2);
    }

    public static double ReadingComprehensionPercentage(int InterviewReading)
    {
        // Calculate the total Reading Interview score
        double result = InterviewReading * .10;

        // Return the result
        return Math.Round(result, 2);
    }
    public static double CommunicationVerbalPercentage(int InterviewCommunication)
    {
        // Calculate the total Communication Interview score
        double result = InterviewCommunication * 0.10;

        // Return the result
        return Math.Round(result, 2);
    }
    public static double AnalyticalAbilityPercentage(int InterviewAnalytical)
    {
        // Calculate the total Analytical Interview score
        double result = InterviewAnalytical * 0.10;

        // Return the result
        return Math.Round(result, 2);
    }

    public static double ReadingCommunicationAnalyticalPercentage(int InterviewReading, int InterviewCommunication, int InterviewAnalytical)
    {
        // Calculate the total equivalent score
        double result = ReadingComprehensionPercentage(InterviewReading) + CommunicationVerbalPercentage(InterviewCommunication) + AnalyticalAbilityPercentage(InterviewAnalytical);

        // Return the result
        return Math.Round(result, 2);
    }

    public static double InterviewResult(int InterviewReading, int InterviewCommunication, int InterviewAnalytical, double GWA)
    {
        // Calculate the total equivalent score
        double result = ReadingCommunicationAnalyticalPercentage(InterviewReading, InterviewCommunication, InterviewAnalytical) + GWAPercentage(GWA);
        // Return the result
        return Math.Round(result, 2);
    }

    public static double OverallTotalRating(int readingRawScore, int mathRawScore, int scienceRawScore, int intelligenceRawScore,
        int InterviewReading, int InterviewCommunication, int InterviewAnalytical, double GWA)
    {
        double result =
           ExaminationResult(readingRawScore, mathRawScore, scienceRawScore, intelligenceRawScore) +
           InterviewResult(InterviewReading, InterviewCommunication, InterviewAnalytical, GWA);
        // Return the result
        return Math.Round(result, 2);
    }
    public static string Remarks(int readingRawScore, int mathRawScore, int scienceRawScore, int intelligenceRawScore,
        int InterviewReading, int InterviewCommunication, int InterviewAnalytical, double GWA)
    {
        string result = OverallTotalRating(readingRawScore, mathRawScore, scienceRawScore, intelligenceRawScore,
            InterviewReading, InterviewCommunication, InterviewAnalytical, GWA)
            >= 75 ? "Passed" : "Failed";
        return result;
    }
}

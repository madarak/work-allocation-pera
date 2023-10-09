namespace WorkAllocationApp.Utils
{
    public static class CalculateHours
    {
        public static double ConsductingLectures(double noOfHrs)
        {
            return noOfHrs;
        }

        public static double PreparationForNewLectures(double noOfLectureHrs, double noOfRepeatingLectureHrs)
        {
            return (noOfLectureHrs - noOfRepeatingLectureHrs) * 3;
        }

        public static double PreparationSameLectures(double noOfLectureHrs, double noOfRepeatingLectureHrs)
        {
            return (noOfLectureHrs - noOfRepeatingLectureHrs) * 1.5;
        }

        public static double ConductingLabSessionByLecturer(double labHrs)
        {
            //T = g1 * w where w = 0.5
            return labHrs * 0.5;
        }

        public static double ConductingLabSessionByInstructor(double labHrs)
        {
            //T = g1 * w where w = 1
            return labHrs * 1;
        }

        public static double PrepararionOfDesignClasses(double designHrs)
        {
            double a1;
            if (designHrs <= 3)
            {
                return designHrs / 3;
            }
            else
            {
                a1 = (designHrs - 3) / 2;
            }
            return 1 + a1; ;
        }

        public static double UndergraduateProjectSupervision(double noOfProjectsS1, double noOfProjectsS2)
        {
            return (noOfProjectsS1 + noOfProjectsS2) * 30;
        }

        public static double SupervisionOfTrainees(double noOfHrs)
        {
            return noOfHrs;
        }

        public static double TrainingReportCorrection(double noOfReports)
        {
            return noOfReports;
        }

        public static double SettingExaminationPapers(double noOfHrsOfThePaper)
        {
            return noOfHrsOfThePaper * 8;
        }

        public static double ModerationOfPapers(double noOfHrsOfThePaper)
        {
            return noOfHrsOfThePaper;
        }

        public static double MarkingSheetsForProblemSolving(double noOfAnswerScripts, double noOfHrs)
        {
            return noOfAnswerScripts * noOfHrs / 10;
        }

        public static double MarkingSheetsForMCQ(double noOfAnswerScripts, double noOfHrs)
        {
            return noOfAnswerScripts * noOfHrs / 20;
        }

        public static double PreparationAndEvaluationQuizzesEasy(double noOfAnswerScripts, double noOfHrs)
        {
            return noOfAnswerScripts * noOfHrs / 6;
        }

        public static double PreparationAndEvaluationQuizzesMCQ(double noOfAnswerScripts, double noOfHrs)
        {
            return noOfAnswerScripts * noOfHrs / 4;
        }

        public static double SettingLabExaminationForDifferentGroups(double noOfGroups)
        {
            return noOfGroups * 3;
        }


        public static double SettingLabExaminationForSameGroups()
        {
            return 3;
        }

        public static double EvaluationOfLabSessions(double noOfStudents, double noOfLabSessions)
        {
            return noOfStudents * noOfLabSessions / 6;
        }

        public static double EvaluationOfDesignWork(double noOfGroups, double noOfFieldSessions)
        {
            return noOfGroups * noOfFieldSessions * 3;
        }

        public static double EvaluationOfPresentation(double hrsOfPresentation)
        {
            return hrsOfPresentation;
        }

        public static double EvaluationOfUGProjectReport(double noOfReports)
        {
            return noOfReports * 3;
        }

        public static double CoordinationOfFieldCamp(double noOfCredits)
        {
            return noOfCredits * 10;
        }

        public static double CoordinationOfTaughtCourseLessThan25()
        {
            return 15;
        }

        public static double CoordinationOfTaughtCourseMoreThan25(double noOfStudents)
        {
            return 1.5 * (noOfStudents - 25);
        }

        public static double StudentTrainingSupport(double noOfStudents)
        {
            return noOfStudents / 12;
        }

        public static double PreparingTrainingAssessments(double noOfVivaSessions)
        {
            return noOfVivaSessions / 6;
        }

        public static double FinalVivaAssessments(double noOfStudents)
        {
            return noOfStudents / 3;
        }

        public static double AdditionalDuties(double noOfHrs)
        {
            return noOfHrs;
        }
    }
}

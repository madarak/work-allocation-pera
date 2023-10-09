import { RoleType, CourseCreditsSummary } from '../constants';

export const calculateTotalLectureHours = (lectureHrs: number) => {
	const preparaionHours = CalculatePreparaionHours(lectureHrs);
	return lectureHrs + preparaionHours;
};

export const calculateTotalTutorialHours = (tutorialHrs: number) => {
	const preparaionHours = CalculatePreparaionHours(tutorialHrs);
	return tutorialHrs + preparaionHours;
};

export const calculateTotalLabHours = (labHrs: number, role: string) => {
	const hrsByRole = calculateTotalLabHoursByRole(labHrs, role);
	return labHrs + hrsByRole;
};

//TODO: check the equation -> t=gw
const calculateTotalLabHoursByRole = (labHrs: number, role: string) => {
	switch (role) {
		case RoleType.HeadOfDepartment:
		case RoleType.SeniorProfessor:
		case RoleType.AssociateProfessor:
		case RoleType.SeniorLecturerGradeIAndII:
		case RoleType.Lecturer:
			return labHrs * 0.5;
		case RoleType.InstructorGradeI:
		case RoleType.InstructorGradeII:
			return labHrs;
	}

	return 0;
};

export const CalculatePreparaionHours = (noOfHrs: number, repeatingHrs: number = 0) => {
	return ((noOfHrs - repeatingHrs) / 2) * 1.5 + ((noOfHrs - repeatingHrs) / 2) * 3;
};

export const CalculateTutorialHours = (noOfHrs: number) => {
	return noOfHrs;
};

export const CalculateLabHours = (noOfHrs: number) => {
	return noOfHrs;
};

export const CalculateAssignmentHours = (noOfHrs: number) => {
	return noOfHrs;
};

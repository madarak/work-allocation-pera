export type Course = {
	courseId: string;
	name: string;
	credits: number;
	lectureHrs: number;
	practicalHrs: number;
	tutorialHrs: number;
	assignementHrs: number;
};

export type Lecturer = {
	lecturerId: string;
	name: string;
	minTeachingHrs: number;
};

export type AllocationCell = {
	AllocationCellId: string;
	AllocationPlanId: string;
	CourseId: string;
	CourseName: string;
	LecturerId: string;
	CreditsAllocation: number;
	LectureHours: number;
	TutorialHours: number;
	LabHours: number; //considered as practicalHrs in department context
	DemonstrationHours: number;
	ClinicalHours: number;
	DiscussionHours: number;
	FieldworkHours: number;
	AssignmentHours: number; //under Undergraduate Academic Administration and Evaluation
};

export type LecturerHoursSummary = {
	lecturerId: string;
	total: number;
	fontColor?: string;
};

export type CourseCreditsSummary = {
	courseName: string;
	creditTotal: number;
	fontColor?: string;
};

export const RoleType = {
	HeadOfDepartment: 'headOfDepartment',
	SeniorProfessor: 'seniorProfessor',
	AssociateProfessor: 'associateProfessor',
	SeniorLecturerGradeIAndII: 'seniorLecturerGradeIAndII',
	Lecturer: 'lecturer',
	InstructorGradeI: 'instructorGradeI',
	InstructorGradeII: 'instructorGradeII',
};

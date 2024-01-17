import React, { useState, useEffect } from 'react';
import { AllocationCell, Course, CourseCreditsSummary, Lecturer, LecturerHoursSummary } from '../constants';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import { Grid, TextField, Typography } from '@mui/material';
import { calculateTotalLectureHours, calculateTotalTutorialHours } from '../Utils/calculations';

type AllocationProps = {
    coursesList: Course[];
    lecturersList: Lecturer[];
    items: any[];
};

const CancelButton = styled(Button)({
    alignSelf: 'end',
    fontSize: 16,
});

export const AllocationPlan = () => {
    const [loading, setLoading] = useState(true);
    const [courses, setCourses] = useState<Course[]>([]);
    const [lecturers, setLecturers] = useState<Lecturer[]>([]);
    const [courseSummaries, setCourseSummaries] = useState<CourseCreditsSummary[]>([]);

    const [lecturerSummaries, setLecturerSummaries] = useState<LecturerHoursSummary[]>([]);
    const [allocationCells, setAllocationCells] = useState<AllocationCell[][]>([]);
    const [count, setCount] = useState<number>(0);

    const populateCourses = async () => {
        const response = await fetch('allocation/courses');
        const data = await response.json();
        setCourses(data);
    };

    const populateLecturers = async () => {
        const response = await fetch('allocation/lecturers');
        const data = await response.json();
        setLecturers(data);
    };

    const populateAllocationCells = async () => {
        const response = await fetch('allocation/initial-allocations');
        const data = await response.json();
        setAllocationCells(data);
    };

    useEffect(() => {
        populateCourses();
        populateLecturers();
        populateAllocationCells();
        setLoading(false);
    }, []);

    const setCourseCredits = (value: string, item: AllocationCell, courseIndex: number) => {
        item.CreditsAllocation = parseFloat(value);

        //calculate sum of the credits for the current course
        const sum = calculateCreditSummary(allocationCells[courseIndex]);

        // const gg = course && course.credits > sum ? '#155ea7' : course && course.credits == sum ? '#1f9b00' : '#dc3545';
        //get the current course
        const course = courses.find((x) => x.name === item.CourseName);
        courseSummaries[courseIndex] = {
            courseName: item.CourseName,
            creditTotal: sum,
            fontColor: course && course.credits > sum ? '#155ea7' : course && course.credits == sum ? '#1f9b00' : '#cf0000',
        };

        setCourseSummaries(courseSummaries);
    };

    const calculateCreditSummary = (array: AllocationCell[]) => {
        let sum = 0;
        for (let i = 0; i < array.length; i++) {
            sum += array[i].CreditsAllocation ? array[i].CreditsAllocation : 0;
        }
        return sum;
    };

    const setLectureHours = (item: AllocationCell, lecturerIndex: number) => {
        //find the current course
        const course = courses.find((x) => x.name === item.CourseName);
        if (course) {
            //calculte the allocated lecture hrs based on the course credit entered
            const lectureHrs = (course.lectureHrs / course.credits) * item.CreditsAllocation;
            item.LectureHours = calculateTotalLectureHours(lectureHrs);

            const tutorialHrs = (course.tutorialHrs / course.credits) * item.CreditsAllocation;
            item.TutorialHours = calculateTotalTutorialHours(tutorialHrs);

            //TODO: check calculation
            const labHrs = (course.practicalHrs / course.credits) * item.CreditsAllocation;
            item.LabHours = labHrs;

            //TODO: check the calculation
            item.AssignmentHours = (course.assignementHrs / course.credits) * item.CreditsAllocation;
        }

        const lecturerArray: AllocationCell[] = [];
        for (let i = 0; i < allocationCells.length; i++) {
            //iterate the rows (by course)
            const courseArray = allocationCells[i];
            const itemCell = courseArray.find((x) => x.LecturerId === item.LecturerId);
            if (itemCell) {
                //add to an array to calculate the sum of lecture hrs
                lecturerArray[i] = itemCell;
            }
        }

        //calculate sum of the lecture hrs for the current lecturer
        const sum = calculateLectureHours(lecturerArray);
        const lecturer = lecturers.find((x) => x.lecturerId == item.LecturerId);

        //set summary for the current changed lecturer hrs
        lecturerSummaries[lecturerIndex] = {
            lecturerId: item.LecturerId,
            total: sum,
            fontColor: lecturer && lecturer.minTeachingHrs > sum ? '#155ea7' : '#1f9b00',
        };

        for (let i = 0; i < lecturers.length; i++) {
            if (!lecturerSummaries[i]) {
                lecturerSummaries[i] = {
                    lecturerId: allocationCells[0][i].LecturerId,
                    total: 0,
                };
            }
        }

        setLecturerSummaries(lecturerSummaries);
    };

    const calculateLectureHours = (array: AllocationCell[]) => {
        let sum = 0;
        for (let i = 0; i < array.length; i++) {
            sum += array[i] && array[i].LectureHours ? array[i].LectureHours : 0;
            sum += array[i] && array[i].TutorialHours ? array[i].TutorialHours : 0;
            sum += array[i] && array[i].LabHours ? array[i].LabHours : 0;
            sum += array[i] && array[i].AssignmentHours ? array[i].AssignmentHours : 0;
        }
        return Math.round(sum * 100) / 100;
    };

    const incrementCounter = () => {
        setCount(count + 1);
    };

    const handleSave = () => {
        fetch('allocation/save', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(allocationCells),
        });
    };

    console.log("allocationCells", allocationCells);

    const renderAllocationPlan = () => {
        return (
            <Grid>
                <Grid item>
                    <table className="table table-striped" aria-labelledby="tableLabel">
                        <thead>
                            <tr>
                                <th></th>
                                {lecturers.map((lecturer, index) => (
                                    <th key={`${lecturer.name}-${index}`}>{lecturer.name}</th>
                                ))}
                                <th>Summary</th>
                            </tr>
                        </thead>
                        <tbody>
                            {courses
                                ? courses.map((course: Course, courseIndex: number) => (
                                    <tr key={courseIndex}>
                                        <td>{course.name}</td>
                                        {allocationCells[courseIndex]
                                            ? allocationCells[courseIndex].map((item: AllocationCell, lecturerIndex) => (
                                                <td key={`${item.CourseName}-${lecturerIndex}`}>
                                                    <TextField
                                                        id={`${item.CourseName}-${lecturerIndex}`}
                                                        variant="outlined"
                                                        type="number"
                                                        onChange={(event) => {
                                                            incrementCounter();
                                                            setCourseCredits(event.target.value, item, courseIndex);
                                                            setLectureHours(item, lecturerIndex);
                                                        }}
                                                    />
                                                </td>
                                            ))
                                            : null}
                                        {courseSummaries && courseSummaries[courseIndex] ? (
                                            <td style={{ color: courseSummaries[courseIndex].fontColor, fontWeight: 700 }}>
                                                {courseSummaries[courseIndex].creditTotal}
                                            </td>
                                        ) : null}
                                    </tr>
                                ))
                                : null}
                        </tbody>
                    </table>
                </Grid>
                <Grid item>
                    <table className="table table-striped" aria-labelledby="tableLabel">
                        <thead>
                            <tr>
                                <th style={{ width: 180 }}></th>
                                {lecturerSummaries
                                    ? lecturerSummaries.map((summary: LecturerHoursSummary, summaryIndex: number) => (
                                        <th style={{ width: 180, color: summary.fontColor }} key={summaryIndex}>
                                            {summary.total}
                                        </th>
                                    ))
                                    : null}
                            </tr>
                        </thead>
                    </table>
                </Grid>
            </Grid>
        );
    };

    return (
        <div>
            <h1 id="tableLabel">Calculate work hours</h1>
            <p>Enter details for work calculation.</p>
            {loading ? (
                <p>
                    <em>Loading...</em>
                </p>
            ) : (
                renderAllocationPlan()
            )}
            <Button variant="contained" color="success" onClick={() => handleSave()}>
                Save
            </Button>
            <CancelButton variant="text">Cancel</CancelButton>
        </div>
    );
};

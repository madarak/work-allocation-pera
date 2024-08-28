import React, { useEffect, useState } from 'react';
import { Button, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography, Box } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faDownload } from '@fortawesome/free-solid-svg-icons';
import { Link } from 'react-router-dom';
import * as XLSX from 'xlsx';

interface Allocation {
    courseCode: string;
    activity: string;
    hoursSpent: number;
}

interface LecturerAllocations {
    lecturerId: string;
    lecturerName: string;
    allocations: Allocation[];
}

export const ViewAllocationPlan = () => {
    const [lecturerAllocations, setLecturerAllocations] = useState<LecturerAllocations[]>([]);

    useEffect(() => {
        async function fetchAllocations() {
            try {
                const response = await fetch('allocation/view-allocation');
                const data: LecturerAllocations[] = await response.json();
                setLecturerAllocations(data);
            } catch (error) {
                console.error('Failed to fetch allocations:', error);
            }
        }

        fetchAllocations();
    }, []);

    const renderAllocations = () => {
        return lecturerAllocations.map((lecturer) => (
            <React.Fragment key={lecturer.lecturerId}>
                {lecturer.allocations.map((allocation, index) => (
                    <TableRow key={index}>
                        {index === 0 && (
                            <>
                                <TableCell rowSpan={lecturer.allocations.length} style={{ border: '2px solid #ccc' }}>
                                    {lecturer.lecturerName}
                                </TableCell>
                            </>
                        )}
                        <TableCell style={{ border: '2px solid #ccc' }}>
                            {allocation.activity} (Includes Preparation Hours)
                        </TableCell>
                        <TableCell style={{ border: '2px solid #ccc' }}>{allocation.courseCode}</TableCell>
                        <TableCell style={{ border: '2px solid #ccc' }}>{allocation.hoursSpent}</TableCell>
                        {index === 0 && (
                            <TableCell rowSpan={lecturer.allocations.length} style={{ border: '2px solid #ccc' }}>
                                <Button
                                    variant="outlined"
                                    size="small"
                                    color="primary"
                                    startIcon={<FontAwesomeIcon icon={faEdit} />}
                                    component={Link}
                                    to={`/edit-allocation/${lecturer.lecturerId}`}
                                >
                                    Edit
                                </Button>
                            </TableCell>
                        )}
                    </TableRow>
                ))}
            </React.Fragment>
        ));
    };

    const exportToExcel = () => {
        const ws = XLSX.utils.json_to_sheet(
            lecturerAllocations.flatMap(lecturer =>
                lecturer.allocations.map(allocation => ({
                    LecturerId: lecturer.lecturerId,
                    LecturerName: lecturer.lecturerName,
                    Activity: allocation.activity,
                    CourseCode: allocation.courseCode,
                    HoursSpent: allocation.hoursSpent
                }))
            )
        );
        const wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Allocations');
        XLSX.writeFile(wb, 'Allocations.xlsx');
    };

    return (
        <Box>
            <TableContainer component={Paper} style={{ margin: '20px', maxHeight: '600px' }}>
                <Table stickyHeader>
                    <TableHead>
                        <TableRow>
                            <TableCell style={{ fontWeight: 'bold', border: '2px solid #ccc' }}>Lecturer Name</TableCell>
                            <TableCell style={{ fontWeight: 'bold', border: '2px solid #ccc' }}>Activity</TableCell>
                            <TableCell style={{ fontWeight: 'bold', border: '2px solid #ccc' }}>Course Code</TableCell>
                            <TableCell style={{ fontWeight: 'bold', border: '2px solid #ccc' }}>Hours Spent</TableCell>
                            <TableCell style={{ fontWeight: 'bold', border: '2px solid #ccc' }}>Edit/Update</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {renderAllocations()}
                    </TableBody>
                </Table>
            </TableContainer>
            <Box display="flex" justifyContent="space-between" alignItems="center" padding="20px">
                <Typography>Click the export button to export it to Excel.</Typography>
                <Button
                    variant="contained"
                    color="primary"
                    startIcon={<FontAwesomeIcon icon={faDownload} />}
                    onClick={exportToExcel}
                >
                    Export
                </Button>
            </Box>
        </Box>
    );
};

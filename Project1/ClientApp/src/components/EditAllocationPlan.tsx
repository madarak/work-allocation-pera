import React, { useState, useEffect } from 'react';
import { Button, TextField, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
import { AllocationCell } from '../constants';

export const EditAllocationPlan = () => {
    const { lecturerId } = useParams();
    const navigate = useNavigate();
    const [allocations, setAllocations] = useState<AllocationCell[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        async function fetchAllocations() {
            try {
                const response = await fetch(`/allocation/get-allocations/${lecturerId}`);
                const data = await response.json();
                setAllocations(data);
                setLoading(false);
            } catch (error) {
                console.error('Failed to fetch allocations:', error);
                setLoading(false);
            }
        }

        fetchAllocations();
    }, [lecturerId]);

    const handleCreditChange = (index: number, event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        if (event.target instanceof HTMLInputElement) {
            const newAllocations = [...allocations];
            newAllocations[index].CreditsAllocation = parseFloat(event.target.value);
            setAllocations(newAllocations);
        }
    };

    const handleSave = async () => {
        try {
            await fetch('/allocation/save', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(allocations),
            });
            navigate('/view-allocation');
        } catch (error) {
            console.error('Failed to save allocations:', error);
        }
    };

    
    return (
        <TableContainer component={Paper} style={{ margin: '20px' }}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Course Name</TableCell>
                        <TableCell>Credits Allocation</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {loading ? (
                        <TableRow>
                            <TableCell colSpan={2}>Loading...</TableCell>
                        </TableRow>
                    ) : (
                        allocations.map((allocation, index) => (
                            <TableRow key={allocation.AllocationCellId}>
                                <TableCell>{allocation.CourseName}</TableCell>
                                <TableCell>
                                    <TextField
                                        type="number"
                                        value={allocation.CreditsAllocation}
                                        onChange={(e) => handleCreditChange(index, e)}
                                    />
                                </TableCell>
                            </TableRow>
                        ))
                    )}
                </TableBody>
            </Table>
            <Button variant="contained" color="primary" onClick={handleSave}>
                Save Changes
            </Button>
        </TableContainer>
    );
};

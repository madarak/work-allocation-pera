import React, { useState, useEffect } from "react";
import { Course, Lecturer } from "../constants";
import Button from "@mui/material/Button";
import { styled } from "@mui/material/styles";

type AllocationProps = {
  coursesList: Course[];
  lecturersList: Lecturer[];
  items: any[];
};

const CancelButton = styled(Button)({
  alignSelf: "end",
  fontSize: 16,
});

export const Allocation = (props: AllocationProps) => {
  const { coursesList } = props;
  console.log("coursesList", coursesList);
  const [loading, setLoading] = useState(true);
  const [forecasts, setForecasts] = useState([]);

  const populateWeatherData = async () => {
    const response = await fetch("allocation/hello");
    const studentData = await fetch("allocation/courses");
    const students = await studentData.json();
    console.log("students", students);
    const data = await response.json();
    setForecasts(data);
    setLoading(false);
  };

  useEffect(() => {
    populateWeatherData();
  }, []);

  const renderForecastsTable = (forecasts: any[]) => {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Madara</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  };

  return (
    <div>
      <h1 id="tableLabel">This is test</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {loading ? (
        <p>
          <em>Loading...</em>
        </p>
      ) : (
        renderForecastsTable(forecasts)
      )}
      <Button variant="contained" color="success">
        Calculate
      </Button>
      <CancelButton variant="text">Cancel</CancelButton>
    </div>
  );
};

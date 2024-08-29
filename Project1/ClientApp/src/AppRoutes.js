/* eslint-disable react/react-in-jsx-scope */
import { Allocation } from "./components/Allocation";
import { AllocationPlan } from "./components/AllocationPlan";
import { ViewAllocationPlan } from "./components/ViewAllocationPlan";
import { EditAllocationPlan } from "./components/EditAllocationPlan";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />,
    },
    {
        path: "/allocation",
        element: <Allocation />,
    },
    {
        path: "/plan",
        element: <AllocationPlan />,
    },
    {
        path: "/view_allocation_plan",
        element: <ViewAllocationPlan />,
    },
    {
        path: "/edit-allocation/:lecturerId",
        element: <EditAllocationPlan />,
    },


];

export default AppRoutes;

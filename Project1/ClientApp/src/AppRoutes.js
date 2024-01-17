/* eslint-disable react/react-in-jsx-scope */
import { Allocation } from "./components/Allocation";
import { AllocationPlan } from "./components/AllocationPlan";
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
];

export default AppRoutes;

import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Allocation } from "./components/Allocation";
import { AllocationPlan } from "./components/AllocationPlan";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/counter",
    element: <Counter />,
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

import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";
import Error404 from "./pages/Error404/Error404";
import Home from "./pages/Home/Home";
import Login from "./pages/Signin/Login";
import Signup from "./pages/Signup/Signup";
const App = () => {
  const token = localStorage.getItem("token");
  return (
    <Router>
      <Routes>
        <Route
          path="/"
          element={token ? <Home /> : <Navigate to="/signin" />}
        />

        <Route
          path="/Login"
          element={!token ? <Login /> : <Navigate to="/" />}
        />

        <Route
          path="/CreateUser"
          element={!token ? <Signup /> : <Navigate to="/" />}
        />
        <Route path="*" element={<Error404 />} />
      </Routes>
    </Router>
  );
};

export default App;

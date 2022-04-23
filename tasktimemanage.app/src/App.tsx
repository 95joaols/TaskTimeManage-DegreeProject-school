import { useEffect, useState } from "react";
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
import { useAppDispatch, useAppSelector } from "./store/hook";
import { defaultState, setUser } from "./store/state/authSlice";
import { UserToken } from "./Types/UserToken";
import jwt from "jwt-decode";

const App = () => {
  const { name } = useAppSelector((state) => state.auth);
  const dispatch = useAppDispatch();

  const [token, setToken] = useState<UserToken>()

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token)
    { 
      const user: UserToken = jwt(token);
      dispatch(setUser({ token: token, name: user.unique_name, id: user.nameid }));  
      setToken(user);
    }
    else
    {
      dispatch(defaultState());
      setToken(undefined);
      }
  }, [name])
  
  console.log("token",token);
  
  
  return (
    <Router>
      <Routes>
        <Route
          path="/"
          element={token ? <Home /> : <Navigate to="/Login" />}
        />

        <Route
          path="/Login"
          element={!token ? <Login /> : <Navigate to="/" />}
        />

        <Route
          path="/Signup"
          element={!token ? <Signup /> : <Navigate to="/" />}
        />
        <Route path="*" element={<Error404 />} />
      </Routes>
    </Router>
  );
};

export default App;


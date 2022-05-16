import jwt from "jwt-decode";
import React, { useEffect, useState } from "react";
import { BrowserRouter as Router, Navigate, Route, Routes } from "react-router-dom";
import Error404 from "./pages/Error404/Error404";
import Home from "./pages/Home/Home";
import Login from "./pages/Signin/Login";
import Signup from "./pages/Signup/Signup";
import { useAppDispatch, useAppSelector } from "./store/hook";
import { defaultState, setUser } from "./store/state/authSlice";
import { UserToken } from "./Types/UserToken";

const App = () => {
    const { username: name } = useAppSelector((state) => state.auth);
    const dispatch = useAppDispatch();

    const [token, setToken] = useState<UserToken>();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            const user: UserToken = jwt(token);
            if (user.exp > new Date().getTime()) {
                dispatch(defaultState());
                setToken(undefined);
                localStorage.removeItem("token");
                return;
            }

            dispatch(setUser({ token: token, name: user.unique_name, id: user.nameid }));
            setToken(user);
        } else {
            dispatch(defaultState());
            setToken(undefined);
        }
    }, [name]);

    return (
        <Router>
            <Routes>
                <Route path="/" element={token ? <Home /> : <Navigate to="/Login" />} />

                <Route path="/Login" element={!token ? <Login /> : <Navigate to="/" />} />

                <Route path="/Signup" element={!token ? <Signup /> : <Navigate to="/" />} />
                <Route path="*" element={<Error404 />} />
            </Routes>
        </Router>
    );
};

export default App;

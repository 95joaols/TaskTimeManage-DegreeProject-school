import {
    BrowserRouter as Router,
    Routes,
    Route,
} from "react-router-dom";
  
function Routing() {
    return (
        <Router>
        <Routes>
            <Route exact path="/login" component={Login} />
            <PrivateRoute path="/">
              <Hooray />
            </PrivateRoute>
          </Routes>
        </Router>
  
    );
}
  
export default Routing;
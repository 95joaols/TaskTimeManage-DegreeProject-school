import {
    BrowserRouter as Router,
    Routes,
    Route,
} from "react-router-dom";
  
function Routing() {
    return (
        <Router>
        <Routes>
            <Route exact path="/">
              <Home />
            </Route>
            <Route path="/about">
            </Route>
            <Route path="/:user">
              <User />
            </Route>
            <Route>
              <NoMatch />
            </Route>
          </Routes>
        </Router>
  
    );
}
  
export default Routing;
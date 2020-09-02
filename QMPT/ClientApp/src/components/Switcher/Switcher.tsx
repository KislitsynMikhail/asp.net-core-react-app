import React from "react";
import { Switch, Route } from "react-router-dom";
import LoginContainer from "../Login/LoginContainer";
import Organizations from "../Organizations/Organizations";
import Price from "../Price/Prices";
import Devices from "../Devices/Devices";

const Switcher = () => {
    return (
        <Switch>
            <Route exact path={"/"} render={() => <div style={{width: '100%', marginTop: 200, marginLeft: 250}}>Пустая страница</div>}/>
            <Route path={"/login"} render={() => <LoginContainer />}/>
            <Route path={"/customers/:view?/:organizationId?"} render={() => <Organizations organizationType={'Customer'} />} />
            <Route path={"/providers/:view?/:organizationId?"} render={() => <Organizations organizationType={'Provider'} />} />
            <Route path={"/price"} render={() => <Price />} />
            <Route path={"/devices/:view?"} render={() => <Devices />} />
        </Switch>
    )
}

export default Switcher
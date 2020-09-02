import {AppState} from "../../redux/store";
import {connect} from "react-redux";
import Header from "./Header";
import {logout} from "../../redux/reducers/tokens-reducer";
import { getIsAuth } from "../../redux/selectors/tokens-selectors";

export type MapStateToProps = {
    isAuth: boolean
}

const mapStateToProps = (state: AppState): MapStateToProps => ({
    isAuth: getIsAuth(state)
})

export type MapDispatchToProps = {
    logout: () => void
}

const HeaderContainer = connect(mapStateToProps, {logout})(Header)

export default HeaderContainer
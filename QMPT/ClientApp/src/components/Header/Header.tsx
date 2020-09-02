import React from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import MenuItem from '@material-ui/core/MenuItem';
import {appName} from "../../helpers/projectConstants";
import {MapDispatchToProps, MapStateToProps} from "./HeaderContainer";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        appBar: {
            zIndex: theme.zIndex.drawer + 1,
        },
        toolBar: {
            display: 'flex',
            justifyContent: 'space-between',
            textAlign: 'center'
        },
        label: {
            textTransform: 'uppercase'
        }
    }),
);

type PropsType = MapStateToProps & MapDispatchToProps

const Header: React.FC<PropsType> = ({isAuth, logout}) => {
    const classes = useStyles()

    const onLogoutClick = () => {
        logout()
    }

    return (
        <AppBar position="fixed" className={classes.appBar}>
            <Toolbar className={classes.toolBar}>
                <Typography variant="h6" noWrap className={classes.label}>
                    {appName}
                </Typography>

                {isAuth && <div>
                    <MenuItem onClick={onLogoutClick}>Выйти из системы</MenuItem>
                </div>}
            </Toolbar>
        </AppBar>
    )
}

export default Header
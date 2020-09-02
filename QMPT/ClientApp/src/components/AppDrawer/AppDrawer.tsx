import React from "react";
import {createStyles, Drawer, Theme, List, ListItemText} from "@material-ui/core";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Divider from "@material-ui/core/Divider";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import InboxIcon from '@material-ui/icons/MoveToInbox';
import Toolbar from '@material-ui/core/Toolbar';
import {AppState} from "../../redux/store";
import { connect } from "react-redux";
import { Redirect, useHistory } from "react-router-dom";
import { getIsAuth } from "../../redux/selectors/tokens-selectors";

const drawerWidth = 200;
const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            display: 'flex',
        },
        appBar: {
            zIndex: theme.zIndex.drawer + 1,
        },
        drawer: {
            width: drawerWidth,
            flexShrink: 0,
        },
        drawerPaper: {
            width: drawerWidth,
        },
        drawerContainer: {
            overflow: 'auto',
        },
        content: {
            flexGrow: 1,
            padding: theme.spacing(3),
        },
        itemDiv: {
            '& :hover': {
                cursor: 'pointer'
            }
        }
    }),
);
type Props = MapStateToProps

const AppDrawer: React.FC<Props> = ({isAuth}) => {

    const classes = useStyles()
    const history = useHistory()

    if (!isAuth) {
        return <Redirect to={"/login"}/>
    }

    const addToURL = (path: string) => {
        history.location.pathname = '/'
        history.push(path)
    }

    const getListItem = (key: string, path: string, text: string) => {
        return (
            <ListItem button key={key} onClick={() => addToURL(path)} className={classes.itemDiv}>
                <ListItemIcon><InboxIcon /></ListItemIcon>
                <ListItemText primary={text} />
            </ListItem>
        )
    }

    return (
        <Drawer
            className={classes.drawer}
            variant="permanent"
            classes={{
                paper: classes.drawerPaper,
            }}
        >
            <Toolbar />
            <div className={classes.drawerContainer}>
                <List>
                    {getListItem('Kek', '', 'Пустая страница')}
                    {getListItem('Customers', 'customers', 'Заказчики')}
                    {getListItem('Providers', 'providers', 'Поставщики')}
                    {getListItem('Price', 'price', 'Прайс')}
                    {getListItem('Devices', 'devices', 'Устройства')}
                </List>
                <Divider />
            </div>
        </Drawer>
    )
}

type MapStateToProps = {
    isAuth: boolean
}
const mapStateToProps = (state: AppState): MapStateToProps => ({
    isAuth: getIsAuth(state)
})

export default connect(mapStateToProps)(AppDrawer)
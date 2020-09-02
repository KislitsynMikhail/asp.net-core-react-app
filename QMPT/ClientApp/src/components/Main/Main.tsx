import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Paper from "@material-ui/core/Paper";
import React from "react";
import {makeStyles} from "@material-ui/core/styles";
import clsx from "classnames";

const drawerWidth = 200;

const useStyles = makeStyles((theme) => ({
    content: {
        margin: `100px 0 0 ${drawerWidth}px`,
    },
    gridContainer: {
        display: 'flex',
    },
    leftGrid: {
        flexBasis: 400,
        flexGrow: 1,
        flexShrink: 0
    },
    rightGrid: {
        flexBasis: 300,
        flexGrow: 0,
        flexShrink: 0
    },
    paper: {
        marginLeft: 20,
        textAlign: 'center'
    }
}));

type PropsType = {
}

const Main: React.FC<PropsType> = () => {
    const classes = useStyles();

    return (
        <Container component={"main"} className={classes.content}>
            <Grid className={classes.gridContainer} spacing={3}>
                <Grid item className={classes.leftGrid}>
                    <Paper className={classes.paper}>Left container</Paper>
                </Grid>
                <Grid item className={classes.rightGrid}>
                    <Paper className={classes.paper}>Right container</Paper>
                </Grid>
            </Grid>
        </Container>
    )
}

export default Main
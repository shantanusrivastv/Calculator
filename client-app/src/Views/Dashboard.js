import React, { useEffect, useCallback } from "react";
import Header from "../Common/Header";
import {
  Button,
  FormLabel,
  Grid,
  makeStyles,
  Paper,
  TextField,
} from "@material-ui/core";
import { Redirect, withRouter } from "react-router-dom";
import { actionTypes } from "../Common/constants";
import axios from "../Common/axios-news";

const useStyles = makeStyles(() => ({
  root: {
    flexGrow: 1,
    background: "#3f51b5",
  },
  rootContainer: {
    height: "100vh",
  },
  container: {
    padding: 8,
  },
  input: {
    padding: "8px 0",
  },
  button: {
    padding: "8px 0",
    textAlign: "right",
  },
}));

const DashBoard = (props) => {
  const { userInfo, authorised, dispatch } = props;
  const classes = useStyles();

  const GetPublisherArtciles = useCallback(() => {
    axios
      .get("Dashboard/GetPublisherDashboard")
      .then((response) => {
        dispatch({
          type: actionTypes.LOAD_ARTICLES,
          payload: response.data,
        });
      })
      .catch((error) => {
        console.error(error);
      });
  }, [dispatch]);

  const GetAllArtciles = useCallback(() => {
    axios
      .get("Dashboard/")
      .then((response) => {
        dispatch({
          type: actionTypes.LOAD_ARTICLES,
          payload: response.data,
        });
      })
      .catch((error) => {
        console.error(error);
      });
  }, [dispatch]);

  if (!authorised) {
    return <Redirect to={"/"} />; //redirect to Login Screen
  }

  return (
    <div className={classes.root}>
      <Header authorised={authorised} dispatch={dispatch}>
        {" "}
      </Header>
      <form className={classes.root} noValidate autoComplete="off">
        <TextField
          id="standard-secondary"
          label="Standard secondary"
          color="secondary"
        />
        <TextField
          id="filled-secondary"
          label="Filled secondary"
          variant="filled"
          color="secondary"
        />
        <TextField
          id="outlined-secondary"
          label="Outlined secondary"
          variant="outlined"
          color="secondary"
        />
      </form>
    </div>
  );
};

export default withRouter(DashBoard);

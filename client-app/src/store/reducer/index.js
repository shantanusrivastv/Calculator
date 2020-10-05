import { actionTypes } from "../../Common/constants";

const saveUserInfo = (state, action) => {
  return {
    ...state,
    userInfo: action.userInfo,
    authorised: true,
  };
};

const logOut = () => {
  sessionStorage.clear();
  window.location.href = "/";
};

const saveArticles = (state, action) => {
  return {
    ...state,
    articles: action.payload,
  };
};


export const reducer = (state, action) => {
  switch (action.type) {
    case actionTypes.USER_LOGIN:
      return saveUserInfo(state, action);
    case actionTypes.LOAD_ARTICLES:
      return saveArticles(state, action);
    case actionTypes.LOGOUT:
      return logOut();
    default:
      return state;
  }
};

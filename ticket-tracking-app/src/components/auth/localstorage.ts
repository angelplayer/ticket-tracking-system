const ID_TOKEN_KEY = "id_token";

export const getUserToken = () => {
  const data = window.localStorage.getItem(ID_TOKEN_KEY);
  const localStorage = data != null ? JSON.parse(data) : data;
  return localStorage;
};

export const saveUserToken = (user) => {
  window.localStorage.setItem(ID_TOKEN_KEY, JSON.stringify(user));
};

export const destroyUserToken = () => {
  window.localStorage.removeItem(ID_TOKEN_KEY);
};

export default { getUserToken, saveUserToken, destroyUserToken };
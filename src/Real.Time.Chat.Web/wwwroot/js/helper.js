var wasmHelper = {};

wasmHelper.ACCESS_TOKEN_KEY = "__access_token__";
wasmHelper.ACCESS_EMAIL = "__access_username__";

wasmHelper.saveAccessToken = function (tokenStr) {
    localStorage.setItem(wasmHelper.ACCESS_TOKEN_KEY,tokenStr);
};

wasmHelper.saveAccessUserName = function (username) {
    localStorage.setItem(wasmHelper.ACCESS_EMAIL, username);
}

wasmHelper.getAccessToken = function () {
    return localStorage.getItem(wasmHelper.ACCESS_TOKEN_KEY);
};

wasmHelper.getAccessUserName = function () {
    return localStorage.getItem(wasmHelper.ACCESS_EMAIL);
};
var wasmHelper = {};

wasmHelper.ACCESS_TOKEN_KEY = "__access_token__";
wasmHelper.ACCESS_EMAIL = "__access_email__";

wasmHelper.saveAccessToken = function (tokenStr) {
    localStorage.setItem(wasmHelper.ACCESS_TOKEN_KEY,tokenStr);
};

wasmHelper.saveAccessEmail = function (email) {
    localStorage.setItem(wasmHelper.ACCESS_EMAIL, email);
}

wasmHelper.getAccessToken = function () {
    return localStorage.getItem(wasmHelper.ACCESS_TOKEN_KEY);
};

wasmHelper.getAccessEmail = function () {
    return localStorage.getItem(wasmHelper.ACCESS_EMAIL);
};
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

base64ToUint8Array = function(base64String) {
    let rawData = window.atob(base64String);
    let len = rawData.length;
    let buffer = new ArrayBuffer(len);
    let outputArray = new Uint8Array(buffer);
    while (--len) {
        outputArray[len] = rawData.charCodeAt(len);
    }
    return outputArray;
}
// 各ページで発生した ime のイベントをbackground.js に渡す
window.addEventListener("imeOn", (e) => {
    console.log("contents.js: call ime on");

    chrome.runtime.sendMessage("imeOn", (response) => {
        console.log("imeOn message processed");
    });
});

window.addEventListener("imeOff", (e) => {
    console.log("contents.js: call ime off");
    chrome.runtime.sendMessage("imeOff", (response) => {
        console.log("imeOn message processed");
    });
});

console.log("contents js initialized");

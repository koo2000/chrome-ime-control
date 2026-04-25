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
window.addEventListener("imemode", (e) => {
    console.log("contents.js: call ime on");
    console.log("contents.js: e = [" + JSON.stringify(e) + "]");
    console.log("contents.js: e.detail = [" + JSON.stringify(e.detail) + "]");

    chrome.runtime.sendMessage({imemode : e.detail.imemode, imeOn : e.detail.imeOn }, (response) => {
        console.log("imemode message processed");
    });
});

console.log("contents js initialized");
window.addEventListener("animalfound", (e) => console.log("animal", e.detail.name));

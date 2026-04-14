var agent = navigator.userAgent;
console.log(agent);


// ポートを開ける
var port = chrome.runtime.connectNative("com.github.koo2000.imecontrol");

port.onMessage.addListener((msg) => {
  // TODO 戻りメッセージの処理（あれば）
  console.log('Received' + msg);
});
port.onDisconnect.addListener(() => {
  // TODO 接続時は再接続（？）
  console.log('Disconnected');
});


// 画面からメッセージを受け取ってネイティブに渡す
chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if ("imeOn" === message) {
    port.postMessage({ "imemode": "on", "agent" : agent });
  } else if ("imeOff" === message) {
    port.postMessage({ "imemode": "off", "agent" : agent  });
  }
  sendResponse("request processed");
});

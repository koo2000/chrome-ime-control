var agent = navigator.userAgent;
console.log("agent = " + agent);

// ポートを開ける
class ImeControlConnection {
  constructor() {
    this.port = null;
    // 画面からメッセージを受け取ってネイティブに渡す
    chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
      var imemode = message.imemode;
      if (this.port == null) {
        this.reconnect();
      }
      if ("imeOn" === imemode) {
        this.port.postMessage({ "imemode": "on", "agent" : agent });
      } else if ("imeOff" === imemode) {
        this.port.postMessage({ "imemode": "off", "agent" : agent  });
      } else if ("hiragana" === imemode) {
        this.port.postMessage({ "imemode": "hiragana", "agent" : agent  });
      } else if ("katakana" === imemode) {
        this.port.postMessage({ "imemode": "katakana", "agent" : agent  });
      } else if ("hankakukana" === imemode) {
        this.port.postMessage({ "imemode": "hankakukana", "agent" : agent  });
      } 
      sendResponse("request processed");
    });

  }
  reconnect() {
    this.port = chrome.runtime.connectNative("com.github.koo2000.imecontrol");
    this.port.onMessage.addListener((msg) => {
      console.log('ImeControlConnection: Received' + msg);
    });
    this.port.onDisconnect.addListener(() => {
      console.log('ImeControlConnection: Disconnected');
      this.port = null;
    });

  }
}


new ImeControlConnection();

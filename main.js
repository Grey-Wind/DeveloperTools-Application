const { app, BrowserWindow } = require('electron');
const path = require('path');

function createWindow() {
  // 创建浏览器窗口
  const mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    // 设置浏览器预设，允许Node.js集成
    webPreferences: {
      // 开启Node.js集成
      nodeIntegration: true,
    },
  });

  // 关闭工具栏
  mainWindow.setMenu(null);

  // 加载应用的 index.html
  mainWindow.loadFile(
    path.join(__dirname, 'page', 'index.html'),
  );
}

// 当所有窗口都关闭时退出应用
app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    // 在 Windows 上，应用不会自动退出
    app.quit();
  }
});

// 在 Linux 上，需要手动设置退出行为
app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) {
    createWindow();
  }
});

// 应用开始运行
app.on('ready', () => {
  console.log('App ready');
  // 创建浏览器窗口
  createWindow();
});

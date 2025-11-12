const fs = require('fs');
const path = require('path');

class DirectoryTree {
  constructor(options = {}) {
    this.options = {
      exclude: options.exclude || ['node_modules', '.git', '.DS_Store'],
      maxDepth: options.maxDepth || 10,
      showHidden: options.showHidden || false,
      ...options
    };
  }

  shouldExclude(item) {
    return this.options.exclude.includes(item) || 
           (!this.options.showHidden && item.startsWith('.'));
  }

  getDirectoryTree(dir, depth = 0, prefix = '', isLast = true) {
    if (depth > this.options.maxDepth) return '';

    try {
      const items = fs.readdirSync(dir);
      const validItems = items
        .filter(item => !this.shouldExclude(item))
        .sort((a, b) => {
          // 目录在前，文件在后
          const aPath = path.join(dir, a);
          const bPath = path.join(dir, b);
          const aIsDir = fs.statSync(aPath).isDirectory();
          const bIsDir = fs.statSync(bPath).isDirectory();
          
          if (aIsDir && !bIsDir) return -1;
          if (!aIsDir && bIsDir) return 1;
          return a.localeCompare(b);
        });

      if (validItems.length === 0) return '';

      let tree = '';
      
      validItems.forEach((item, index) => {
        const itemPath = path.join(dir, item);
        const isItemLast = index === validItems.length - 1;
        const stats = fs.statSync(itemPath);
        const isDirectory = stats.isDirectory();

        // 当前行的连接符和前缀
        const connector = isItemLast ? '└── ' : '├── ';
        const newPrefix = prefix + (isLast ? '    ' : '│   ');

        // 添加图标
        const icon = isDirectory ? '📁 ' : this.getFileIcon(item);
        tree += prefix + connector + icon + item + '\n';

        if (isDirectory) {
          tree += this.getDirectoryTree(itemPath, depth + 1, newPrefix, isItemLast);
        }
      });

      return tree;
    } catch (error) {
      return prefix + '└── [权限不足或读取错误]\n';
    }
  }

  getFileIcon(filename) {
    const ext = path.extname(filename).toLowerCase();
    const icons = {
      '.js': '📄 ',
      '.json': '📋 ',
      '.html': '🌐 ',
      '.css': '🎨 ',
      '.md': '📝 ',
      '.txt': '📃 ',
      '.png': '🖼️ ',
      '.jpg': '🖼️ ',
      '.jpeg': '🖼️ ',
      '.gif': '🖼️ ',
      '.svg': '🖼️ ',
    };
    return icons[ext] || '📄 ';
  }

  generate() {
    const currentDir = process.cwd();
    const dirName = path.basename(currentDir);
    
    console.log(`\n📁 ${dirName}/`);
    console.log(this.getDirectoryTree(currentDir));
    
    // 显示统计信息
    this.showStats(currentDir);
  }

  showStats(dir) {
    let fileCount = 0;
    let dirCount = 0;

    const countItems = (dirPath, depth = 0) => {
      if (depth > this.options.maxDepth) return;

      try {
        const items = fs.readdirSync(dirPath);
        
        items.forEach(item => {
          if (this.shouldExclude(item)) return;
          
          const itemPath = path.join(dirPath, item);
          const stats = fs.statSync(itemPath);
          
          if (stats.isDirectory()) {
            dirCount++;
            countItems(itemPath, depth + 1);
          } else {
            fileCount++;
          }
        });
      } catch (error) {
        // 忽略权限错误
      }
    };

    countItems(dir);
    console.log(`\n📊 统计信息:`);
    console.log(`   目录: ${dirCount} 个`);
    console.log(`   文件: ${fileCount} 个`);
    console.log(`   总计: ${dirCount + fileCount} 个项目\n`);
  }
}

// 使用方法
const tree = new DirectoryTree({
  exclude: ['node_modules', '.git', '.DS_Store', 'dist', 'build','obj','.github','bin'],
  maxDepth: 5,
  showHidden: false
});

tree.generate();
var express = require("express");
var router = express.Router();
const nodeCmd = require("node-cmd");
// const { spawn } = require('child_process');
const { exec, spawn } = require('child_process');
const bat = spawn('cmd.exe', ['/c', 'dir']);
/* GET home page. */
router.post("/", function (req, res, next) {
  let command = req.body.command
  let gitpull = req.body.gitpull
  let tofol = `
  cd C:\\Inspec\\InspecWeb
  git pull
  iisreset /stop
  dotnet publish InspecWeb.csproj -o publish
  iisreset /start
  `
  let pull = `git pull
  `
  let deployProd = `
  cd "C://Inspec\InspecWeb"
  iisreset /stop
  dotnet publish InspecWeb.csproj -o publish
  iisreset /start
  `
  nodeCmd.get(deployProd , (err, data, stderr) => res.json(data)
  // res.json(tofol)
    // res.render("index", { title: data.toString() })
  );
});

router.get("/", function (req, res, next) {
  // let command = req.body.command
  exec('deploy.bat', (err, stdout, stderr) => {
    if (err) {
      res.json(err);
      return;
    }
    res.json(stdout);
  });

  // nodeCmd.get(command, (err, data, stderr) =>
  //   //  res.json(data)
  //   res.render("index", { title: data.toString() })
  // );
});

router.get("/ddd", function (req, res, next) {
 
  exec('deploy.bat', (err, stdout, stderr) => {
    if (err) {
      res.json(err);
      return;
    }
    res.json(stdout);
  });
});

module.exports = router;

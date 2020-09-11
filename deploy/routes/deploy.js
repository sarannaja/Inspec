var express = require("express");
var router = express.Router();
const nodeCmd = require("node-cmd");
/* GET home page. */
router.post("/", function (req, res, next) {
  let command = req.body.command
  let gitpull = req.body.gitpull
  let tofol = `
  cd C:\Inspec\InspecWeb
  `
  let pull = `git pull
  `
  let deployProd = `
  iisreset /stop
  dotnet publish InspecWeb.csproj -o publish
  iisreset /start
  `

  nodeCmd.get(gitpull ?tofol+pull+deployProd : tofol+deployProd , (err, data, stderr) => res.json(data)
    // res.render("index", { title: data.toString() })
  );
});

router.get("/", function (req, res, next) {
  let command = req.body.command


  nodeCmd.get(command, (err, data, stderr) =>
    //  res.json(data)
    res.render("index", { title: data.toString() })
  );
});

module.exports = router;

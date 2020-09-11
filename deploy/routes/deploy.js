var express = require("express");
var router = express.Router();
const nodeCmd = require("node-cmd");
/* GET home page. */
router.get("/", function (req, res, next) {
  nodeCmd.get("dir", (err, data, stderr) =>
    res.render("index", { title: data.toString() })
  );
});

module.exports = router;
const nodeCmd = require("node-cmd");
nodeCmd.get("dir", (err, data, stderr) => console.log(data));

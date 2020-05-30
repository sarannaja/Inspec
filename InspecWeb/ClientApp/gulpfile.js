const { src, dest } = require('gulp');
var replace = require('gulp-replace')

exports.default = function (cb) {    
    src(['dist/ng-project/index.html'])
        .pipe(replace('type="module"', 'type="text/javascript"'))
        .pipe(dest('dist/ng-project/'));
    cb();
};
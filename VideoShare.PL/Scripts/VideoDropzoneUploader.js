var dropZ = Dropzone.options.dropzoneForm = {
    maxFiles: 1,
    maxFilesize: 1000,
    acceptedFiles: "video/mp4",
    init: function () {
        this.on("maxfilesexceeded", function (file) {
            this.removeAllFiles();
            this.addFile(file);
        });        

        this.on("addedfile", function (file) {
            var removeButton = Dropzone.createElement("<button>Remove file</button>");
            var _this = this;
            removeButton.addEventListener("click", function (e) {
                e.preventDefault();
                e.stopPropagation();
                _this.removeFile(file);
            });
            file.previewElement.appendChild(removeButton);
        });
    }
};
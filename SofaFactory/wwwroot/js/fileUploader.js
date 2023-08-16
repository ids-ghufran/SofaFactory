
var fp_styles = document.createElement('style');

fp_styles.innerHTML = `
 
        .file-uploader-container {
            padding: 5px;
            min-height: 250px;
            border: 2px dashed lightgray;
            border-radius: 10px;
            /* margin-bottom: 20px; */
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
        }

        .file-controls {
            position: relative;
            top: 30px;
            display: flex;
            flex-direction: column;
            width: 220px;
            text-align: center;
        }

        .file-uploader-container .image-preview {
            position: absolute;
            top: 9px;
            left: 25px;
            display: flex;
            justify-content: flex-start;
            gap: 20px;
            width: 90%;
        }

        .file-uploader-container .image-item {
            width: 70px;
            position: relative;
        }

        .file-uploader-container .delete-btn {
            border: 0px;
            height: 20px;
            width: 20px;
            background: red;
            color: white;
            border-radius: 5px;
            font-size: 10px;
            position: absolute;
            top: 0;
            right: -5px;
        }

        label.error, span.error {
            color: red;
        }
  
`;
function getImageDimensions(file) {
    return new Promise((resolve, reject) => {
        if (!file) {
            // No file selected
            resolve(undefined);
        }
        const reader = new FileReader();

        reader.onload = function (event) {
            const image = new Image();

            image.onload = function () {
                    resolve({ width: image.width, height: image.height });
            };

            image.src = event.target.result;
        };

        reader.readAsDataURL(file);
    });
}

function validateFileType(file) {
    var validTypes = new Set(['jpeg', 'jpg', 'webp', 'png', 'svg']);
    var fileName = file.name;
    let temp = fileName.split('.');
    let ext = temp[temp.length - 1];
    return validTypes.has(ext);
}

function validateAspectRatio(fileInput, expectedAspectRatio) {
    return new Promise((resolve, reject) => {
        const file = fileInput;

        if (!file) {
            // No file selected
            resolve(false);
        }

        const reader = new FileReader();

        reader.onload = function (event) {
            const image = new Image();

            image.onload = function () {
                const aspectRatio = image.width / image.height;
                // Compare the aspect ratio with the expected aspect ratio (e.g., 16:9)
                if (Math.abs(aspectRatio - expectedAspectRatio) > 0.01) {
                    // Invalid aspect ratio
                    resolve(false);
                } else {
                    // Valid aspect ratio
                    resolve(true);
                }
            };

            image.src = event.target.result;
        };

        reader.readAsDataURL(file);
    });
}

class FileUploader {
       constructor(_opt) {
           let opt = { id: "", fileCount: 5, aspectRatio: 1, errorHandler: undefined, imageDimension: undefined , ..._opt }
        this.elementId = opt.id;
        this.files = [];
        this.errorHandler = opt.errorHandler;
        this.fileCount = opt.fileCount;
           
        this.fileIp = document.querySelector(`#${opt.id}`);
           this.renderUi(this.init);
           this.height = opt.imageDimension?.height;
           this.width = opt.imageDimension?.width;
           this.aspectRatio = opt.imageDimension?.width && opt.imageDimension?.height ? opt.imageDimension.width / opt.imageDimension.height: opt.aspectRatio;
    }
    init(self) {
        self.input = self.fileIp.querySelector("#file-input");
        self.btn = self.fileIp.querySelector("button");
        self.label = self.fileIp.querySelector(".file-control-label");
        self.previewElement = self.fileIp.querySelector(".image-preview");
        self.fileIp.addEventListener("dragover", (e) => self.handleDragOver(e));
        self.fileIp.addEventListener("dragleave", (e) => self.handleDragLeave(e));
        self.fileIp.addEventListener("drop", (e) => self.handleDrop(e));
        self.btn.type = "button";
        self.btn.addEventListener("click", () => self.input.click());
        self.input.addEventListener("change", (e) => self.handleFileSelect(e));
        console.log("label_c > ", self.label);
    }
    async renderUi(initialize) {
        document.querySelector('head').append(fp_styles);
        this.fileIp.innerHTML = `
                        <div class="image-preview"></div>
                            <div class="file-controls">
                                <input type="file" class="file-input d-none" id="file-input" />
                                <h5 class="file-control-label">Drag And Drop the files</h5>
                                <button class="btn btn-light border">Select File</button>
                            </div>
`;
        this.fileIp.classList.add("file-uploader-container");

        setTimeout(()=>initialize(this), 500);
    
    }
    getFile() {
        return this.files;
    }
    getFileCount() {
        return this.files.length;
    }
    handleFileSelect(e) {
        this.handleFiles(e.target.files);
        setTimeout(() => { e.target.value = null }, 1000)
    }
    handleDragOver(e) {
        e.preventDefault();
        this.label.innerHTML = "Drop";
    }
    handleDrop(e) {
        e.preventDefault();
        this.label.innerHTML = "Drag And Drop the files";
        const files = e.dataTransfer.files;
        this.handleFiles(files);
    }
    handleDragLeave(e) {
        e.preventDefault();
        //console.log("leave", label);
        this.label.innerHTML = "Drag And Drop the files";
    }
    updatePreview() {
        this.previewElement.innerHTML = "";
        for (const file of this.files) {
            this.createFilePreview(file);
        }
    }
    createFilePreview(file) {
        console.log("file", file);
        const previewImage = document.createElement("img");
        previewImage.src = URL.createObjectURL(file.file);
        previewImage.alt = file.name;
        previewImage.style.width = "100%";
        var parent = document.createElement("div");
        parent.classList.add("image-item");
        var deleteBtn = document.createElement('button')
        deleteBtn.classList.add("delete-btn");
        deleteBtn.innerHTML = "<i class='fa fa-trash'></i>";
        deleteBtn.type = "button";
        deleteBtn.addEventListener("click", () => this.deleteFile(file));
        parent.appendChild(previewImage);
        parent.appendChild(deleteBtn);
        this.previewElement.appendChild(parent);
    }
    deleteFile(file) {
        this.files = this.files.filter((f) => f.id !== file.id);
        this.updatePreview();
    }
    async handleFiles(files) {
        if (this.files.length >= this.fileCount || (this.files.length + files.length > this.fileCount)) {
            files = undefined;
            alert(`Only ${this.fileCount} files can be uploaded`);
            return;
        }
        for (const file of files) {
            let length = this.files.length;
            let validtype = validateFileType(file);
            let validshape = validtype ? await validateAspectRatio(file, this.aspectRatio) : false;
            let dim = await getImageDimensions(file);
            if (validtype && validshape) {
                let f = { "name": file.name, file: file, id: length == 0 ? length + 1 : this.files[length - 1].id + 1 };
                this.files.push(f);
                this.createFilePreview(f);
            }
            else {
                if (!validtype) {
                    if (!this.errorHandler)
                        alert(`Please make sure that ${file.name} is image file.`);
                    else
                        this.errorHandler(`Please make sure that ${file.name} is image file.`)
                }
                else {
                    if (!this.errorHandler)
                        if (this.height && this.width) {
                            alert(`Please make sure that image height = ${dim.width / this.aspectRatio} & width = ${dim.width}.`);
                        }
                        else {
                            alert(`Please make sure that image file  have aspect ratio of ${this.aspectRatio}.`);
                        }
                    else {

                        if (this.height && this.width) {
                            this.errorHandler(`Please make sure that image height = ${dim.width / this.aspectRatio} & width = ${dim.width}.`);
                        } else {
                            this.errorHandler(`Please make sure that image file  have aspect ratio of ${this.aspectRatio}.`);
                        }

                    }
                }
            }
        }
    }
}
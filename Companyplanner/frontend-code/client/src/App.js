import React, { useEffect, useState } from "react";
import axios from "axios";
import { Carousel } from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";

axios.defaults.baseURL = process.env.REACT_APP_API_URL || "http://localhost:7125";

function App() {
  const [customerId, setCustomerId] = useState(1);
  const [images, setImages] = useState([]);
  const [files, setFiles] = useState([]);

  useEffect(() => {
    fetchImages();
  }, [customerId]);

  const fetchImages = async () => {
    try {
      const res = await axios.get(`/Customer/images?customerId=${customerId}`);
      setImages(res.data.customerImages || []);
    } catch (err) {
      console.error("Error fetching images:", err);
    }
  };

  const handleFileChange = (e) => {
    setFiles([...e.target.files]);
  };

  const uploadImages = async () => {
    if (files.length === 0) {
      alert("Select images first");
      return;
    }
    const formData = new FormData();
    formData.append("customerId", customerId);
    files.forEach((file) => formData.append("files", file));

    try {
      await axios.post("/Customer/images/upload", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      setFiles([]);
      fetchImages();
    } catch (err) {
      console.error("Upload failed:", err);
    }
  };

  const deleteImage = async (imageId) => {
    try {
      await axios.delete(`/Customer/images/${imageId}?customerId=${customerId}`);
      setImages((prev) => prev.filter((img) => img.id !== imageId));
    } catch (err) {
      console.error("Delete failed:", err);
    }
  };

  return (
    <div
      style={{
        minHeight: "100vh",
        backgroundColor: "#121212",
        color: "#fff",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
        padding: "30px",
      }}
    >
      <h2 style={{ marginBottom: "20px" }}>Customer {customerId} Images</h2>

      {/* Upload */}
      <div style={{ marginBottom: "20px" }}>
        <input type="file" multiple onChange={handleFileChange} />
        <button
          onClick={uploadImages}
          style={{
            marginLeft: "10px",
            padding: "8px 16px",
            backgroundColor: "#007bff",
            color: "#fff",
            border: "none",
            borderRadius: "6px",
            cursor: "pointer",
          }}
        >
          Upload
        </button>
      </div>

      {/* Carousel */}
      {images.length > 0 ? (
        <div style={{ width: "700px" }}>
          <Carousel
            showThumbs={false}
            dynamicHeight={false}
            infiniteLoop
            useKeyboardArrows
            autoPlay={false}
            statusFormatter={(current, total) => `${current} of ${total}`}
            renderArrowPrev={(onClickHandler, hasPrev) =>
              hasPrev && (
                <button
                  type="button"
                  onClick={onClickHandler}
                  style={{
                    position: "absolute",
                    zIndex: 2,
                    top: "40%",
                    left: 15,
                    background: "rgba(0,0,0,0.6)",
                    border: "none",
                    color: "#fff",
                    fontSize: "24px",
                    cursor: "pointer",
                    borderRadius: "50%",
                    width: "40px",
                    height: "40px",
                  }}
                >
                  ‹
                </button>
              )
            }
            renderArrowNext={(onClickHandler, hasNext) =>
              hasNext && (
                <button
                  type="button"
                  onClick={onClickHandler}
                  style={{
                    position: "absolute",
                    zIndex: 2,
                    top: "40%",
                    right: 15,
                    background: "rgba(0,0,0,0.6)",
                    border: "none",
                    color: "#fff",
                    fontSize: "24px",
                    cursor: "pointer",
                    borderRadius: "50%",
                    width: "40px",
                    height: "40px",
                  }}
                >
                  ›
                </button>
              )
            }
          >
            {images.map((img) => (
              <div key={img.id}>
                <img
                  src={`data:image/jpeg;base64,${img.imageBase64}`}
                  alt="Customer"
                  style={{
                    maxHeight: "450px",
                    objectFit: "contain",
                    borderRadius: "8px",
                    background: "#000",
                  }}
                />
                {/* fixed delete button */}
                <div style={{ marginTop: "15px" }}>
                  <button
                    onClick={() => deleteImage(img.id)}
                    style={{
                      padding: "6px 14px",
                      backgroundColor: "#dc3545",
                      color: "#fff",
                      border: "none",
                      borderRadius: "6px",
                      cursor: "pointer",
                    }}
                  >
                    Delete
                  </button>
                </div>
              </div>
            ))}
          </Carousel>
        </div>
      ) : (
        <p>No images found.</p>
      )}
    </div>
  );
}

export default App;

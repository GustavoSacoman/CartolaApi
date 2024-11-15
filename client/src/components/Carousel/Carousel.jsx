import React, { useState, useEffect, useRef } from 'react';
import './Carousel.css';

const Carousel = ({ slides, autoPlayInterval = 5000 }) => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const slideCount = slides.length;
  const timeoutRef = useRef(null);

  useEffect(() => {
    startAutoPlay();
    return () => {
      stopAutoPlay();
    };
  }, [currentIndex]);

  const startAutoPlay = () => {
    stopAutoPlay();
    timeoutRef.current = setTimeout(() => {
      handleNext();
    }, autoPlayInterval);
  };

  const stopAutoPlay = () => {
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
    }
  };

  const handleNext = () => {
    setCurrentIndex((prevIndex) => (prevIndex + 1) % slideCount);
  };

  const handlePrev = () => {
    setCurrentIndex((prevIndex) => (prevIndex - 1 + slideCount) % slideCount);
  };

  const handleDotClick = (index) => {
    setCurrentIndex(index);
  };

  return (
    <div className="custom-carousel">
      <div
        className="custom-carousel__track"
        style={{ transform: `translateX(-${currentIndex * 100}%)` }}
      >
        {slides.map((slide, index) => (
          <div key={index} className="custom-carousel__slide">
            <img src={slide.image} alt={slide.title} className="custom-carousel__slide-img" />
            <div className="custom-carousel__slide-content">
              <h2>{slide.title}</h2>
              <p>{slide.description}</p>
            </div>
          </div>
        ))}
      </div>

      {/* Navigation Buttons */}
      <button
        className="custom-carousel__button custom-carousel__button--prev"
        onClick={handlePrev}
        type="button"
        aria-label="Previous slide"
      >
        ‹
      </button>
      <button
        className="custom-carousel__button custom-carousel__button--next"
        onClick={handleNext}
        type="button"
        aria-label="Next slide"
      >
        ›
      </button>

      {/* Navigation Dots */}
      <div className="custom-carousel__dots">
        {slides.map((_, index) => (
          <button
            key={index}
            className={`custom-carousel__dot ${index === currentIndex ? 'active' : ''}`}
            onClick={() => handleDotClick(index)}
            aria-label={`Go to slide ${index + 1}`}
          />
        ))}
      </div>
    </div>
  );
};

export default Carousel;

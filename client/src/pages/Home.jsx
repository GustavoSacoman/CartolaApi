import Carousel from '../components/Carousel/Carousel';
import Sidebar from '../components/Sidebar/Sidebar';
import slide1 from '../assets/slide1.png';
import slide2 from '../assets/slide2.png';
import slide3 from '../assets/slide3.png';
import slide4 from '../assets/slide4.png';

const slides = [
  {
    image: slide1,
    alt: "Description 1"
  },
  {
    image: slide2,
    alt: "Description 2"
  },
  {
    image: slide3,
    alt: "Description 3"
  },
  {
    image: slide4,
    alt: "Description 4"
  }
];

const Home = () => {
  return (
    <div>
        <Sidebar />
        <Carousel slides={slides} />
    </div>
  );
};

export default Home;